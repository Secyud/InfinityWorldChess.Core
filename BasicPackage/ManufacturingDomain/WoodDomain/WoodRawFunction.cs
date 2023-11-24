using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;

namespace InfinityWorldChess.ManufacturingDomain.WoodDomain
{
    public class WoodRawFunction : EquipmentRawSelect
    {
        protected override IList<IItem> GetSelectList()
        {
            return GameScope.Instance.Player.Role
                .Item.All()
                .Where(u => u is WoodEquipmentRaw)
                .ToList();
        }

        protected override void ChangeSelect(IItem select)
        {
            base.ChangeSelect(select);
            WoodProcessFunction function = Manufacture.Get<WoodProcessFunction>();
            function.ReselectRaw(select as WoodEquipmentRaw);
        }
    }
}