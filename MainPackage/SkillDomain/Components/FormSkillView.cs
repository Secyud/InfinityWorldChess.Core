#region

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public sealed class FormSkillView : SkillViewBase<FormSkillCell, FormSkillView>
	{
		protected override int CellCount => MainPackageConsts.FormSkillCount;

		public override void BindCell(FormSkillCell cell)
		{
			base.BindCell(cell);
			cell.Bind(Property?.FormSkill[cell.CellIndex]?.FormSkill);
		}
		protected override void InitData()
		{
			for (int i = 0; i < MainPackageConsts.FormSkillCount; i++)
			{
				Cells[i].Bind(Property.FormSkill[i]?.FormSkill);
			}
		}
	}
}