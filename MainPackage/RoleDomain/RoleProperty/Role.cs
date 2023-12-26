#region

using System.Runtime.InteropServices;
using Secyud.Ugf;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    [Guid("2B27EE43-4A3A-52DA-4E5F-8A83CE5E8912")]
    public partial class Role : IHasContent
    {
        public int Id { get; set; }

        public PropertyCollection<Role, IRoleProperty> Properties { get; }

        public string ShowName => Basic.Name;

        public string ShowDescription => Basic.Description;

        public Role()
        {
            Properties = new PropertyCollection<Role, IRoleProperty>(this);
        }

        public void SetContent(Transform transform)
        {
            transform.AddTitle1(ShowName);
            transform.AddParagraph(ShowDescription);
        }

        public void Save(IArchiveWriter writer)
        {
            writer.Write(Id);
            Basic.Save(writer);
            Nature.Save(writer);
            Buffs.Save(writer);
            Item.Save(writer);
            Equipment.Save(writer);
            CoreSkill.Save(writer);
            FormSkill.Save(writer);
            PassiveSkill.Save(writer);
            Relation.Save(writer);
            Properties.Save(writer);
        }

        public void Load(IArchiveReader reader, WorldCell position)
        {
            Id = reader.ReadInt32();
            Basic.Load(reader);
            Nature.Load(reader);
            Buffs.Load(reader);
            Item.Load(reader);
            Equipment.Load(reader, this);
            CoreSkill.Load(reader);
            FormSkill.Load(reader);
            PassiveSkill.Load(reader, this);
            Relation.Load(reader, this, position);
            Properties.Load(reader);
        }
    }
}