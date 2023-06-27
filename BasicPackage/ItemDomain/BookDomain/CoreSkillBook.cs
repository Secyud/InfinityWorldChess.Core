#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.BasicBundle.CoreSkills;

#endregion

namespace InfinityWorldChess.ItemDomain.BookDomain
{
    public class CoreSkillBook : SkillBookBase
    {
        protected override void Reading(Role role)
        {
            CoreSkillTemplate skill = Create<CoreSkillTemplate>(SkillName);
            role.CoreSkill.LearnedSkills.Add(skill);
        }
    }
}