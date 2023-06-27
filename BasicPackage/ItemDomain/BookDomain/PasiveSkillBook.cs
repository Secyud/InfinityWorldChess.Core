#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.BasicBundle.PassiveSkills;

#endregion

namespace InfinityWorldChess.ItemDomain.BookDomain
{
    public class PassiveSkillBook : SkillBookBase
    {
        protected override void Reading(Role role)
        {
            PassiveSkillTemplate skill = Create<PassiveSkillTemplate>(SkillName);
            role.PassiveSkill.LearnedSkills.Add(skill);
        }
    }
}