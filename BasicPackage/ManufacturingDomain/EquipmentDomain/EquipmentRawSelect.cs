using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ManufacturingDomain.Components;
using InfinityWorldChess.Ugf;

namespace InfinityWorldChess.ManufacturingDomain.EquipmentDomain
{
    public abstract class EquipmentRawSelect : Select<IItem, ItemSorters, ItemFilters>
    {
        public override void OnInitialize()
        {
            EquipmentFlow flow = Manufacture.Get<EquipmentFlow>();
            flow.SetFlowAction(0,BeforeProcess);
            flow.SetFlowAction(256,AfterProcess);
            flow.SetCheckAction(0,CheckValid);
            flow.SetClearAction(0,ClearRaw);
        }

        private void ClearRaw()
        {
            Manufacture.Get<EquipmentFlow>()
                .GetItemProvider().Remove(SelectedItem);
            
            ChangeSelect(null);
        }
        
        private void BeforeProcess(EquipmentProcessData data)
        {
            if (SelectedItem is not EquipmentManufacturable raw)
                return;
            
            raw.BeforeManufacturing(Manufacture,data);

            for (int i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
                data.Property[i] = raw.Property[i];

            for (int i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
                data.Shape[i] = raw.Shape[i];
        }

        private void AfterProcess(EquipmentProcessData data)
        {
            EquipmentManufacturable raw = SelectedItem as EquipmentManufacturable;
            
            raw?.AfterManufacturing(Manufacture,data);
        }

        private bool CheckValid()
        {
            if (SelectedItem is not null)
                return true;
            "请选择材料".CreateTipFloatingOnCenter();
            return false;
        }
    }
}