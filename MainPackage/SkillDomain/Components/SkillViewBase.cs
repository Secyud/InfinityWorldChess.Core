#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.EditorComponents;

#endregion

namespace InfinityWorldChess.SkillDomain
{
    public abstract class SkillViewBase<TSkillCell, TSkillView> : EditorBase<Role>
        where TSkillView : SkillViewBase<TSkillCell, TSkillView>
        where TSkillCell : SkillCell<TSkillCell, TSkillView>
    {
        protected abstract int CellCount { get; }

        protected TSkillCell[] Cells;

        public TSkillCell[] SkillCells { get; private set; }
        
        protected virtual void Awake()
        {
            Cells = new TSkillCell[CellCount];
        }

        protected override void ClearData()
        {
            for (int i = 0; i < CellCount; i++)
                Cells[i].Bind(null);
        }

        public virtual void BindCell(TSkillCell cell)
        {
            Cells[cell.CellIndex] = cell;
        }
    }
}