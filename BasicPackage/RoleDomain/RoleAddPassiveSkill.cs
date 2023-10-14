using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddPassiveSkill:RoleItemFunctionBase<IPassiveSkill>,IHasDescription
    {
        public  string ShowDescription  => $"可习得内功{Name}。";

        public override void Invoke(Role role)
        {
            if (role.PassiveSkill.LearnedSkills.ContainsKey(Name))
            {
                Debug.LogWarning($"{Name} is already exist;");
                return;
            }
            base.Invoke(role);
        }

        protected override void Invoke(Role role, IPassiveSkill item)
        {
            role.PassiveSkill.LearnedSkills[item.ShowName] = item;
        }
    }
}