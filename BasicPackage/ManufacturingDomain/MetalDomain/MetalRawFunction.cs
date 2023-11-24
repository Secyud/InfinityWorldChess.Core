using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;

namespace InfinityWorldChess.ManufacturingDomain.MetalDomain
{
    public class MetalRawFunction:EquipmentRawSelect
    {
        protected override void ChangeSelect(IItem select)
        {
            base.ChangeSelect(select);
            MetalProcessFunction board = Manufacture.Get<MetalProcessFunction>();
            board.ReselectRaw(select as MetalEquipmentRaw);
        }

        protected override IList<IItem> GetSelectList()
        {
            return GameScope.Instance.Player.Role
                .Item.All()
                .Where(u=>u is MetalEquipmentRaw)
                .ToList();
        }
    }
}