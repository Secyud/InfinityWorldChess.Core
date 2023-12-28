#region

using System.Diagnostics.CodeAnalysis;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	/// <summary>
	/// Skill Container 用来装载技能，虽然技能有自己的属性，
	/// 但是它们在一些特性的加持下可能会装载在不同的地方，
	/// 使用这个描述它们装载的位置。
	/// </summary>
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