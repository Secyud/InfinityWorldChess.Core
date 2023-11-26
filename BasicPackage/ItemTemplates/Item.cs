using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ItemTemplates
{
    public class Item : IItem, IArchivable
    {
        [field: S(0)] public string Name { get; set; }
        [field: S(1)] public string Description { get; set; }
        [field: S(32)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(2)] public int Score { get; set; }
        [field: S(0)] public string ResourceId { get; set; }
        public int SaveIndex { get; set; }

        public virtual void SetContent(Transform transform)
        {
            transform.AddItemHeader(this);
        }

        public virtual void Save(IArchiveWriter writer)
        {
            this.SaveResource(writer);
        }

        public virtual void Load(IArchiveReader reader)
        {
            this.LoadResource(reader);
        }

        public virtual void SaveShown(IArchiveWriter writer)
        {
            writer.Write(Name);
            writer.Write(Description);
            writer.WriteObject(Icon);
        }

        public virtual void LoadShown(IArchiveReader reader)
        {
            Name = reader.ReadString();
            Description = reader.ReadString();
            Icon = reader.ReadObject<IObjectAccessor<Sprite>>();
        }
    }
}