using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.Equipments
{
    public class EquipmentBlueprint : IShowable, IDataResource, IArchivable, IHasContent
    {
        private CustomEquipment _equipment;
        [field: S(1)] public string Name { get; set; }
        [field: S(1)] public string ResourceId { get; set; }
        [field: S(2)] public string Description { get; set; }
        [field: S(5)] public string EquipmentId { get; set; }
        [field: S(32)] public IObjectAccessor<Sprite> Icon { get; set; }

        public CustomEquipment InitEquipment()
        {
            _equipment ??= new CustomEquipment();

            U.Tm.LoadObjectFromResource(_equipment, EquipmentId);

            return _equipment;
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
            _equipment ??= InitEquipment();
            _equipment.TrySetContent(transform);
        }
    }
}