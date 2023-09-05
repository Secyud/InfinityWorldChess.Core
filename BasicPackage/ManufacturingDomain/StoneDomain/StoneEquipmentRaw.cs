using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.StoneDomain
{
    public class StoneEquipmentRaw : EquipmentManufacturable
    {
        [field: S] public byte Volume { get; set; }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddParagraph("锻造格:".Point() + Volume);
        }
    }
}