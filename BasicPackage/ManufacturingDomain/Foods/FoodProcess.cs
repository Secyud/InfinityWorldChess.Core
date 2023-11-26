#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Foods
{
    public  class FoodProcess :  IShowable, IHasContent
    {
        [field: S(1)] public string Name  { get; set; }
        [field: S(2)] public string Description { get; set; }
        [field: S(3)] public byte Length { get; set; }
        [field: S(31)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(31)] public IObjectAccessor<Sprite> Cell { get; set; }
        [field: S(32)] public IActionable<CustomFood> Effect { get; set; }
        
        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public void Process(CustomFood food, FoodMaterial material)
        {
            material.Attach(Effect);
            Effect.Invoke(food);
        }
    }
}