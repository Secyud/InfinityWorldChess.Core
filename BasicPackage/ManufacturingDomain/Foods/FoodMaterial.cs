#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Foods
{
    public class FoodMaterial : Item,IAttachProperty
    {
        [field:S(6)]public byte Living { get; set; }
        [field:S(6)]public byte Kiling { get; set; }
        [field:S(6)]public byte Nimble { get; set; }
        [field:S(6)]public byte Defend { get; set; }
        [field:S(7)]public byte Length { get; set; }
        [field:S(32)]public IActionable<CustomFood> StartEffects { get; set; }
        [field:S(32)]public IActionable<CustomFood> FinishEffects { get; set; }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddShapeProperty(this);
        }

        public void StartFoodManufacturing(CustomFood food)
        {
            this.Attach(StartEffects);
            StartEffects?.Invoke(food);
        }
        public void FinishFoodManufacturing(CustomFood food)
        {
            this.Attach(FinishEffects);
            FinishEffects?.Invoke(food);
        }
    }
}