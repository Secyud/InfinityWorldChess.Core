using InfinityWorldChess.ItemTemplates;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ManufacturingDomain.Foods
{
    public class FoodBlueprint : Item
    {
        private CustomFood _food;
        [field: S] public string FoodId { get; set; }

        public CustomFood InitFood()
        {
            _food ??= new CustomFood();

            U.Tm.LoadObjectFromResource(_food,FoodId);

            return _food;
        }
    }
}