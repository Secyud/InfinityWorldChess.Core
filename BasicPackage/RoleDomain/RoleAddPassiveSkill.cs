using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddPassiveSkill:RoleItemFunctionBase<IPassiveSkill>,IHasDescription
    {
        public  string Description  => $"可习得内功{Name}。";


        protected override void Invoke(Role role, IPassiveSkill skill)
        {
            role.PassiveSkill.TryAddLearnedSkill(skill);
        }
    }
}