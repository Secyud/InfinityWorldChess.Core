using InfinityWorldChess.BasicBundle.Items;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddCoreSkill:RoleItemFunctionBase
    {
        public override string Description=> $"可习得招式{Name}。";
        public override void Invoke(Role role)
        {
            if (U.Tm.Create(ClassId, Name) is ICoreSkill item)
            {
                role.CoreSkill.LearnedSkills.Add(item);
            }
        }
    }
}