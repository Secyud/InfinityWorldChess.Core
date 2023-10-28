using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddFormSkill:RoleItemFunctionBase<IFormSkill>,IHasDescription
    {
        public  string Description => $"可习得阵势{Name}。";

        public override void Invoke(Role role)
        {
            if (role.FormSkill.LearnedSkills.ContainsKey(Name))
            {
                Debug.LogWarning($"{Name} is already exist;");
                return;
            }
            base.Invoke(role);
        }

        protected override void Invoke(Role role, IFormSkill item)
        {
            role.FormSkill.LearnedSkills[item.Name] = item;
        }
    }
}