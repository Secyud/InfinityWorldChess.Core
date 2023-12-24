#region

using System.Linq;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.EditorComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
    public class EquipmentEditor : EditorBase<Role>
    {
        [SerializeField] private ShownCell Cell;

        protected override void InitData()
        {
            Cell.BindShowable(Property.Equipment.Get());
        }

        protected override void ClearUi()
        {
            Cell.BindShowable(null);
        }

        public void OnEquipmentCellClick()
        {
            if (Property.Equipment.Get() is null)
            {
                GlobalScope.Instance.OpenSelect().AutoSetSingleSelectTable
                    <IItem, ItemSorters, ItemFilters>(
                        Property.Item.All().Where(ValidEquipment).ToList(),
                        SetEquipment
                    );
            }
            else
            {
                SetEquipment(null);
            }
        }

        private bool ValidEquipment(IItem arg)
        {
            return arg is IEquipment;
        }

        private void SetEquipment(IItem item)
        {
            IEquipment equipment = item as IEquipment;
            Property.SetEquipment(equipment);
            Cell.BindShowable(equipment);
        }
    }
}