using System.Linq;
using InfinityWorldChess.BattleDomain.BattleRoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.BattleDomain.BattleSkillDomain
{
    public class SkillObservedService : ObservedService
    {
        private readonly RoleObservedService _roleObservedService;

        public SkillObservedService(RoleObservedService roleObservedService)
        {
            _roleObservedService = roleObservedService;
            roleObservedService.AddObserverItem(nameof(SkillObservedService), AutoReselectSkill);
        }

        private SkillContainer _skill;

        public SkillContainer Skill
        {
            get => _skill;
            set
            {
                if (_skill == value)
                    return;
                _skill = value;
                Refresh();
            }
        }

        public void AutoReselectSkill()
        {
            Skill = GetSkillAvailable();
        }

        private SkillContainer GetSkillAvailable()
        {
            BattleRole role = _roleObservedService.Role;
            if (role is null) return null;

            CoreSkillContainer[] coreSkills = role.NextCoreSkills;
            FormSkillContainer[] formSkills = role.NextFormSkills;

            if (_skill is not null &&
                _skill.Skill.CheckCastCondition(role) is null)
                if (coreSkills.Any(u => u == _skill) ||
                    formSkills.Any(u => u == _skill))
                    return _skill;

            for (int i = 0; i < SharedConsts.CoreSkillCodeCount; i++)
                if (coreSkills[i] is not null &&
                    coreSkills[i].CoreSkill.CheckCastCondition(role) is null)
                    return coreSkills[i];

            for (int i = 0; i < SharedConsts.FormSkillTypeCount; i++)
                if (formSkills[i] is not null &&
                    formSkills[i].FormSkill.CheckCastCondition(role) is null)
                    return formSkills[i];

            return null;
        }
    }
}