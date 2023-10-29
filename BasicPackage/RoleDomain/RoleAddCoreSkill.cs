using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddCoreSkill : RoleItemFunctionBase<ICoreSkill>,IHasDescription
    {
        public  string Description => $"可习得招式{Name}。";

        protected override void Invoke(Role role, ICoreSkill item)
        {
            role.CoreSkill.TryAddLearnedSkill(item);
        }
    }
}