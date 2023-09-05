#region

using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.MetalDomain
{
    public abstract class MetalEquipmentProcess : EquipmentProcessBase
    {
        [field: S ] public byte Length { get; set; }
        public byte StartPosition { get; set; }
        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddParagraph($"{U.T["占用时长"]}: {Length}");
        }
    }
}