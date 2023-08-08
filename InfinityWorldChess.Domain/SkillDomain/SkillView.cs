#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.EditorComponents;

#endregion

namespace InfinityWorldChess.SkillDomain
{
    public abstract class SkillView<TSkillCell, TSkillView> : EditorBase<Role>
        where TSkillView : SkillView<TSkillCell, TSkillView>
        where TSkillCell : SkillCell<TSkillCell, TSkillView>
    {
        protected abstract int CellCount { get; }

        protected SkillCell<TSkillCell, TSkillView>[] Cells;

        public SkillCell<TSkillCell, TSkillView>[] SkillCells => Cells;
        
        protected virtual void Awake()
        {
            Cells = new SkillCell<TSkillCell, TSkillView>[CellCount];
        }

        public virtual void SetCell(SkillCell<TSkillCell, TSkillView> cell)
        {
            Cells[cell.CellIndex] = cell;
        }
    }
}