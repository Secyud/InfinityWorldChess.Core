using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddFormSkill:RoleItemFunctionBase
    {
        public override string Description => $"可习得阵势{Name}。";

        public override void Invoke(Role role)
        {
            if (U.Tm.Create(ClassId, Name) is IFormSkill item)
            {
                role.FormSkill.LearnedSkills.Add(item);
            }
        }
    }
}