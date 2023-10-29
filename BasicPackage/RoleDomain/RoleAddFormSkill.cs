using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddFormSkill:RoleItemFunctionBase<IFormSkill>,IHasDescription
    {
        public  string Description => $"可习得阵势{Name}。";


        protected override void Invoke(Role role, IFormSkill item)
        {
            role.FormSkill.TryAddLearnedSkill(item);
        }
    }
}