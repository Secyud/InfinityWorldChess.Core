using System.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAddPassiveSkill:RoleItemFunctionBase
    {
        public override string Description  => $"可习得内功{Name}。";

        public override bool Invoke(Role role)
        {
            SortedDictionary<string, IPassiveSkill> learnedSkills = role.PassiveSkill.LearnedSkills;
            if (learnedSkills.ContainsKey(Name) ||
                U.Tm.ConstructFromResource(ClassId, Name) is not IPassiveSkill item) 
                return false;
            learnedSkills[item.ShowName] = item;
            return true;
        }
    }
}