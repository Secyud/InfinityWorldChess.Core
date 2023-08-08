namespace InfinityWorldChess.SkillDomain
{
    public sealed class CoreSkillView : SkillView<CoreSkillCell, CoreSkillView>
    {
        protected override int CellCount => SharedConsts.CoreSkillCount;

        protected override void InitData()
        {
            for (int i = 0; i < CellCount; i++)
            {
                CoreSkillCell cell = (CoreSkillCell)Cells[i];
                cell.Bind(Property.CoreSkill.Get(cell.Layer, cell.Code).CoreSkill);
            }
        }

        protected override void ClearUi()
        {
            for (int i = 0; i < CellCount; i++)
                Cells[i].Bind(null);
        }
    }
}