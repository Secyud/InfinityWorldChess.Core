#region

using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.StoneDomain
{
    public abstract class StoneEquipmentProcess :
        EquipmentProcessBase
    {
        public short Position { get; set; }
        [field: S ]
        public byte RangeStart { get;set; }
        [field: S ]
        public byte RangeEnd { get; set;}

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddParagraph($"{U.T["目标槽位"]}: {RangeStart}-{RangeEnd}");
        }
    }
}