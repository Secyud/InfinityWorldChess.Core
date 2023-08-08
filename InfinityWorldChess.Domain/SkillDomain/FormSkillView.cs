#region

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public sealed class FormSkillView : SkillView<FormSkillCell, FormSkillView>
	{
		protected override int CellCount => SharedConsts.FormSkillCount;

		
		protected override void InitData()
		{
			for (int i = 0; i < CellCount; i++)
				Cells[i].Bind(Property.FormSkill[i]?.FormSkill);
		}

		protected override void ClearUi()
		{
			for (int i = 0; i < CellCount; i++)
				Cells[i].Bind(null);
		}
	}
}