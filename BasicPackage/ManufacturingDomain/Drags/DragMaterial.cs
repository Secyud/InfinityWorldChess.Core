#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Drags
{
    public class DragMaterial : Item, IAttachProperty,IOverloadedItem
    {
        [field: S(6)] public byte Living { get; set; }
        [field: S(6)] public byte Kiling { get; set; }
        [field: S(6)] public byte Nimble { get; set; }
        [field: S(6)] public byte Defend { get; set; }
        [field: S(7)] public byte Length { get; set; }

        [field: S(31)] public IObjectAccessor<Sprite> Cell { get; set; }
        [field: S(33)] public IActionable<CustomDrag> Effect { get; set; }
        public int Quantity { get; set; }

        public void Process(CustomDrag drag)
        {
            this.Attach(Effect);
            Effect.Invoke(drag);
        }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddParagraph($"占用格数：{Length}");
            transform.AddShapeProperty(this);
        }
    }
}