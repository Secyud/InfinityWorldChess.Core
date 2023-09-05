#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.EditorComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
    public class EquipmentEditor : EditorBase<Role>
    {
        private EquipmentCell[] _equipmentCells;

        private void Awake()
        {
            _equipmentCells = new EquipmentCell[SharedConsts.MaxBodyPartsCount];
        }

        public void SetCell(EquipmentCell cell)
        {
            _equipmentCells[cell.CellIndex] = cell;
        }

        protected override void InitData()
        {
            foreach (EquipmentCell c in _equipmentCells)
            {
                c.BindEquipment(Property.Equipment[c.Index]);
            }
        }

        protected override void ClearUi()
        {
            foreach (EquipmentCell c in _equipmentCells)
            {
                c.BindEquipment(null);
            }
        }
    }
}