#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Drags
{
    public class DragMaterial : Item,IAttachProperty
    {
        [field:S(6)]public byte Living { get; set; }
        [field:S(6)]public byte Kiling { get; set; }
        [field:S(6)]public byte Nimble { get; set; }
        [field:S(6)]public byte Defend { get; set; }
        [field:S(7)]public byte Length { get; set; }
        [field:S(32)]public IActionable<CustomDrag> StartEffects { get; set; }
        [field:S(32)]public IActionable<CustomDrag> FinishEffects { get; set; }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddShapeProperty(this);
        }

        public void StartDragManufacturing(CustomDrag drag)
        {
            this.Attach(StartEffects);
            StartEffects?.Invoke(drag);
        }
        public void FinishDragManufacturing(CustomDrag drag)
        {
            this.Attach(FinishEffects);
            FinishEffects?.Invoke(drag);
        }
    }
}