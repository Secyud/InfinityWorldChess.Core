using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillFunctions
{
    public class AddBuffPassive:BattlePassive
    {
        [field: S] public IObjectAccessor<SkillBuff> Buff { get; set; }
        private SkillBuff _template;
        private SkillBuff Template => _template ??= Buff.Value;
        public override string Description => Template.Description;
        protected IPassiveSkill Skill { get; set; }
        public override void Equip(Role role, IPassiveSkill skill)
        {
            base.Equip(role, skill);
            Skill = skill;
        }

        public override void OnBattleInitialize(BattleRole chess)
        { 
            SkillBuff buff = Buff.Value;
            buff.SetProperty(Skill);
            chess.Buff.Install(buff);
        }
    }
}