#region

using System.Collections.Generic;
using System.Linq;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BuffDomain
{
    public class CustomizableItem<TTarget> : IdBuffProperty<TTarget>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IObjectAccessor<Sprite> Icon { get; set; }

        public override void Save(IArchiveWriter writer)
        {
            writer.Write(Name);
            writer.Write(Description);
            writer.WriteNullable(Icon);
            base.Save(writer);
        }

        public override void Load(IArchiveReader reader)
        {
            Name = reader.ReadString();
            Description = reader.ReadString();
            Icon = reader.ReadNullable<IObjectAccessor<Sprite>>();
            base.Load(reader);
        }
    }
}