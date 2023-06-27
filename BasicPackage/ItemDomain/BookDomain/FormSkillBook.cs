#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.BasicBundle.FormSkills;

#endregion

namespace InfinityWorldChess.ItemDomain.BookDomain
{
	public class FormSkillBook : SkillBookBase
	{
		protected override void Reading(Role role)
		{
			FormSkillTemplate skill = Create<FormSkillTemplate>(SkillName);
			role.FormSkill.LearnedSkills.Add(skill);
		}
	}
}