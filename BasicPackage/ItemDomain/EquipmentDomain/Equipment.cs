#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using System.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
	public class Equipment : IdBuffProperty<Equipment>, IEquipment, IArchivableShown,IArchivable
	{
		public readonly List<IBuff<Role>> RoleBuff = new();
		public readonly List<IBuff<BattleRole>> BattleRoleBuff = new();

		public string Name { get; set; }

		public string Description { get; set; }

		public IObjectAccessor<Sprite> Icon { get; set; }

		public string ShowName => "装备".PointAfter() + Name;

		public string ShowDescription => Description;

		public IObjectAccessor<Sprite> ShowIcon => Icon;

		public int Antique { get; set; }

		public int Score { get; set; }

		public byte TypeCode { get; set; }

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
			this.SaveShown(writer);
			for (int i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
				writer.Write(Property[i]);
			base.Save(writer);
		}

		public override void Load(IArchiveReader reader)
		{
			Antique = reader.ReadInt32();
			TypeCode = reader.ReadByte();
			this.LoadShown(reader);
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