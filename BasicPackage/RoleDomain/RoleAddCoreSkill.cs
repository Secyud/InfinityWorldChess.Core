using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddCoreSkill : RoleItemFunctionBase<ICoreSkill>,IHasDescription
    {
        public  string Description => $"可习得招式{Name}。";

        public override void Invoke(Role role)
        {
            if (role.CoreSkill.LearnedSkills.ContainsKey(Name))
            {
                Debug.LogWarning($"{Name} is already exist;");
                return;
            }
            base.Invoke(role);
        }

        protected override void Invoke(Role role, ICoreSkill item)
        {
            role.CoreSkill.LearnedSkills[item.Name] = item;
        }
    }
}