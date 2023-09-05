#region

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public sealed class FormSkillView : SkillViewBase<FormSkillCell, FormSkillView>
	{
		protected override int CellCount => SharedConsts.FormSkillCount;

		public override void BindCell(FormSkillCell cell)
		{
			base.BindCell(cell);
			cell.Bind(Property?.FormSkill[cell.CellIndex]?.FormSkill);
		}
	}
}