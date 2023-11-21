using System.Linq;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
    public class EquipmentCell : ShownCell
    {
        [SerializeField] private EquipmentEditor Editor;
        public byte Index => (byte)CellIndex;

        private void Awake()
        {
            Editor.SetCell(this);
        }

        public void BindEquipment(IEquipment equipment)
        {
            base.BindShowable(equipment);
        }

        public void OnEquipmentCellClick()
        {
            if (Editor.Property.Equipment[Index] is null)
            {
                GlobalScope.Instance.OpenSelect().AutoSetSingleSelectTable
                    <IItem, ItemSorters, ItemFilters>(
                        Editor.Property.Item.Where(ValidEquipment).ToList(),
                        SetEquipment
                    );
            }
            else
            {
                SetEquipment(null);
            }
        }

        private bool ValidEquipment(IItem item)
        {
            return item is IEquipment e && e.Location == Index;
        }

        private void SetEquipment(IItem item)
        {
            IEquipment equipment = item as IEquipment;
            Editor.Property.SetEquipment(equipment);
            BindEquipment(equipment);
        }
    }
}