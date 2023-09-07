using System.Collections.Generic;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddFormSkill:RoleItemFunctionBase
    {
        public override string Description => $"可习得阵势{Name}。";

        public override bool Invoke(Role role)
        {
            SortedDictionary<string, IFormSkill> learnedSkills = role.FormSkill.LearnedSkills;
            if (learnedSkills.ContainsKey(Name) ||
                U.Tm.ConstructFromResource(ClassId, Name) is not IFormSkill item) 
                return false;
            learnedSkills[item.ShowName] = item;
            return true;

        }
    }
}