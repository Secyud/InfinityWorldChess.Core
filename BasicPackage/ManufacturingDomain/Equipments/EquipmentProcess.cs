#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Equipments
{
    public class EquipmentProcess : IShowable, IHasContent, IArchivable, IDataResource
    {
        [field: S(1)] public string Name { get; set; }
        [field: S(1)] public string ResourceId { get; set; }
        [field: S(2)] public string Description { get; set; }
        [field: S(3)] public byte Length { get; set; }
        [field: S(31)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(31)] public IObjectAccessor<Sprite> Cell { get; set; }
        [field: S(32)] public IActionable<CustomEquipment> Effect { get; set; }

        public void Process(CustomEquipment equipment, EquipmentMaterial material)
        {
            material.TryAttach(Effect);
            Effect?.Invoke(equipment);
        }

        public void Save(IArchiveWriter writer)
        {
            this.SaveResource(writer);
        }

        public void Load(IArchiveReader reader)
        {
            this.LoadResource(reader);
        }

        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
            transform.AddParagraph($"占用格数：{Length}");
            Effect.TrySetContent(transform);
        }
    }
}