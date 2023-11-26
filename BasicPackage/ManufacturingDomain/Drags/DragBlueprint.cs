using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.Drags
{
    public class DragBlueprint : IShowable, IDataResource, IArchivable, IHasContent
    {
        private CustomDrag _drag;
        [field: S(1)]public string ResourceId { get; set; }
        [field: S(1)] public string Name { get; set; }
        [field: S(2)] public string Description { get; set; }
        [field: S(5)] public byte Length { get; set; }
        [field: S(6)] public string DragId { get; set; }
        [field: S(32)] public IObjectAccessor<Sprite> Icon { get; set; }

        public CustomDrag InitDrag()
        {
            _drag ??= new CustomDrag();
            U.Tm.LoadObjectFromResource(_drag, DragId);
            return _drag;
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
            transform.AddParagraph($"拥有格数{Length}");
            _drag ??= InitDrag();
            _drag.TrySetContent(transform);
        }
    }
}