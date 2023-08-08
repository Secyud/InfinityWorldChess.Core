#region

#endregion

namespace InfinityWorldChess.SkillDomain
{
    public sealed class PassiveSkillView : SkillView<PassiveSkillCell,PassiveSkillView>
    {

        protected override int CellCount => SharedConsts.PassiveSkillCount;

        protected override void InitData()
        {
            for (int i = 0; i < CellCount; i++)
                Cells[i].Bind(Property.PassiveSkill[i]);
        }

        protected override void ClearUi()
        {
            for (int i = 0; i < CellCount; i++)
                Cells[i].Bind(null);
        }
    }
}