using System.Ugf;
using InfinityWorldChess.Ugf;

namespace InfinityWorldChess.ManufacturingDomain.EquipmentDomain
{
    public class EquipmentNameInput : ManufactureComponentBase
    {
        public override string Name => nameof(EquipmentNameInput);

        public string ItemName { get; set; }

        public override void OnInitialize()
        {
            EquipmentFlow flow = Manufacture.Get<EquipmentFlow>();

            flow.SetFlowAction(512, SetName);
            flow.SetCheckAction(512, Check);
        }

        private void SetName(EquipmentProcessData data)
        {
            data.Equipment.Name = ItemName;
        }

        private bool Check()
        {
            if (!ItemName.IsNullOrWhiteSpace() &&
                ItemName.Length <= 16)
                return true;

            "装备名称不能为空且不能超过8个字符。".CreateTipFloatingOnCenter();
            return false;
        }
    }
}