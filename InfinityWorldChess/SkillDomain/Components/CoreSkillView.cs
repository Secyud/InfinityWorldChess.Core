namespace InfinityWorldChess.SkillDomain
{
    public sealed class CoreSkillView : SkillViewBase<CoreSkillCell, CoreSkillView>
    {
        protected override int CellCount => SharedConsts.CoreSkillCount;
        public override void BindCell(CoreSkillCell cell)
        {
            base.BindCell(cell);
            cell.Bind(Property?.CoreSkill.Get(cell.Layer, cell.Code).CoreSkill);
        }
    }
}