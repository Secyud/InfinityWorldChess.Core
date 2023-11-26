using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.Foods
{
    public class FoodBlueprint : IShowable, IDataResource, IArchivable, IHasContent
    {
        private CustomFood _food;
        [field: S(1)] public string Name { get; set; }
        [field: S(1)] public string ResourceId { get; set; }
        [field: S(2)] public string Description { get; set; }
        [field: S(5)] public string FoodId { get; set; }
        [field: S(32)] public IObjectAccessor<Sprite> Icon { get; set; }

        public CustomFood InitFood()
        {
            _food ??= new CustomFood();

            U.Tm.LoadObjectFromResource(_food, FoodId);

            return _food;
        }

        public void Save(IArchiveWriter writer)
        {
            this.SaveResource(writer);
        }

        public void Load(IArchiveReader reader)
        {
            this.LoadResource(reader);
        }

        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
            _food ??= InitFood();
            _food.TrySetContent(transform);
        }
    }
}