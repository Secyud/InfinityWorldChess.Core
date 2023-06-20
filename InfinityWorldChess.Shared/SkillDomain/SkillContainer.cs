#region

using System.Diagnostics.CodeAnalysis;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public abstract class SkillContainer
	{
		protected SkillContainer(
			[NotNull] IActiveSkill skill,
			byte equipLayer,
			byte equipCode,
			bool isCoreSkill)
		{
			Skill = skill;
			EquipLayer = equipLayer;
			EquipCode = equipCode;
			IsCoreSkill = isCoreSkill;
		}

		public IActiveSkill Skill { get; }

		public byte EquipLayer { get; }

		public byte EquipCode { get; }
		
		public bool IsCoreSkill { get; }
	}
}