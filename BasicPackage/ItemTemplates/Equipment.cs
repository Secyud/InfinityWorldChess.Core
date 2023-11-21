#region

using System.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemTemplates
{
    public class Equipment : CustomizableItem<Equipment>, IEquipment, IArchivable
    {
        public readonly List<IBuff<Role>> RoleBuff = new();
        public readonly List<IBuff<BattleRole>> BattleRoleBuff = new();


        public int Antique { get; set; }

        public int Score { get; set; }

        public byte TypeCode { get; set; }
        public byte Location { get; set; }

        public int[] Property { get; } = new int[SharedConsts.EquipmentPropertyCount];

        public int SaveIndex { get; set; }

        public void Equip(Role role)
        {
            foreach (IBuff<Role> buff in RoleBuff)
                buff.Install(role);
        }

        public void UnEquip(Role role)
        {
            foreach (IBuff<Role> buff in RoleBuff)
                buff.UnInstall(role);
        }

        public void InitBattle(BattleRole role)
        {
            foreach (IBuff<BattleRole> buff in BattleRoleBuff)
                buff.UnInstall(role);
        }

        protected override Equipment Target => this;

        public override void Save(IArchiveWriter writer)
        {
            writer.Write(Antique);
            writer.Write(TypeCode);
            writer.Write(Location);
            for (int i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
                writer.Write(Property[i]);
            base.Save(writer);
        }

        public override void Load(IArchiveReader reader)
        {
            Antique = reader.ReadInt32();
            TypeCode = reader.ReadByte();
            Location = reader.ReadByte();
            for (int i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
                Property[i] = reader.ReadInt32();
            base.Load(reader);
        }

        public void SetContent(Transform transform)
        {
            transform.AddItemHeader(this);
            transform.AddEquipmentProperty(this);
            transform.AddListShown("装备特效", Values);
        }
    }
}