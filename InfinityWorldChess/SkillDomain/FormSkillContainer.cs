#region

using JetBrains.Annotations;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class FormSkillContainer : SkillContainer
	{
		public FormSkillContainer([NotNull] IFormSkill skill, byte equipLayer, byte equipFullCode)
			: base(skill, equipLayer, equipFullCode, false)
		{
			FormSkill = skill;
		}

		public IFormSkill FormSkill { get; }
	}
}