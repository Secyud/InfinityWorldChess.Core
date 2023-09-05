using InfinityWorldChess.BasicBundle.Items;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddPassiveSkill:RoleItemFunctionBase
    {
        public override string Description  => $"可习得内功{Name}。";

        public override void Invoke(Role role)
        {
            if (U.Tm.Create(ClassId, Name) is IPassiveSkill item)
            {
                role.PassiveSkill.LearnedSkills.Add(item);
            }
        }
    }
}