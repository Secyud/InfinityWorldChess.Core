using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;

namespace InfinityWorldChess.ManufacturingDomain.StoneDomain
{
    public class StoneRawFunction : EquipmentRawSelect
    {
        protected override IList<IItem> GetSelectList()
        {
            return GameScope.Instance.Player.Role
                .Item.All()
                .Where(u => u is StoneEquipmentRaw)
                .ToList();
        }

        protected override void ChangeSelect(IItem select)
        {
            base.ChangeSelect(select);

            StoneProcessFunction board = Manufacture.Get<StoneProcessFunction>();
            board.ReselectRaw(select as StoneEquipmentRaw);
        }
    }
}