#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Foods
{
    public class FoodMaterial : Item,IAttachProperty,IOverloadedItem
    {
        [field:S(6)]public byte Living { get; set; }
        [field:S(6)]public byte Kiling { get; set; }
        [field:S(6)]public byte Nimble { get; set; }
        [field:S(6)]public byte Defend { get; set; }
        [field:S(7)]public byte Length { get; set; }
        [field:S(32)]public IActionable<CustomFood> StartEffects { get; set; }
        [field:S(32)]public IActionable<CustomFood> FinishEffects { get; set; }

        public int Quantity { get; set; } = 1;

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddShapeProperty(this);
            transform.AddParagraph($"拥有格数{Length}");
            StartEffects?.TrySetContent(transform);
            FinishEffects?.TrySetContent(transform);
        }

        public void StartFoodManufacturing(CustomFood food)
        {
            this.TryAttach(StartEffects);
            StartEffects?.Invoke(food);
        }
        public void FinishFoodManufacturing(CustomFood food)
        {
            this.TryAttach(FinishEffects);
            FinishEffects?.Invoke(food);
        }
    }
}