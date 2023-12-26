#region

#endregion

namespace InfinityWorldChess.SkillDomain
{
    public sealed class PassiveSkillView : SkillViewBase<PassiveSkillCell, PassiveSkillView>
    {
        protected override int CellCount => MainPackageConsts.PassiveSkillCount;

        public override void BindCell(PassiveSkillCell cell)
        {
            base.BindCell(cell);
            cell.Bind(Property?.PassiveSkill[cell.CellIndex]);
        }

        protected override void InitData()
        {
            for (int i = 0; i < MainPackageConsts.PassiveSkillCount; i++)
            {
                Cells[i].Bind(Property.PassiveSkill[i]);
            }
        }
    }
}