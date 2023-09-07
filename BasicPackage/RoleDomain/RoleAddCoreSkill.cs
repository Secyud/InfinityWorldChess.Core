using System.Collections.Generic;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddCoreSkill : RoleItemFunctionBase
    {
        public override string Description => $"可习得招式{Name}。";

        public override bool Invoke(Role role)
        {
            var learnedSkills = role.CoreSkill.LearnedSkills;
            if (learnedSkills.ContainsKey(Name) ||
                U.Tm.ConstructFromResource(ClassId, Name) is not CoreSkill item) 
                return false;
            learnedSkills[item.ShowName] = item;
            return true;
        }
    }
}