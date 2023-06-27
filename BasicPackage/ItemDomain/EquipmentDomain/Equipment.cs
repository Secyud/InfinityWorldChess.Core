#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
	public class Equipment : ParasitiferProperty<Equipment>, IEquipment, IArchivableShown,IArchivable
	{
		private readonly List<IBuff<Role>> _addedBuffsRecord = new();
		private readonly List<IBuffFactory<Role>> _roleBuffFactories = new();
		private IArchivableShown _archivableShownImplementation;

		public string Name { get; set; }

		public string Description { get; set; }

		public IObjectAccessor<Sprite> Icon { get; set; }

		public string ShowName => "装备".PointAfter() + Name;

		public string ShowDescription => Description;

		public IObjectAccessor<Sprite> ShowIcon => Icon;

		public int Antique { get; set; }

		public byte Score { get; set; }

		public byte TypeCode { get; set; }

		public byte EquipCode { get; set; }

		public int[] Property { get; } = new int[SharedConsts.EquipmentPropertyCount];


		public int SaveIndex { get; set; }


		public void Equip(Role role)
		{
			foreach (IBuff<Role> buff in _roleBuffFactories.Select(buffFactory => buffFactory.Get()))
			{
				_addedBuffsRecord.Add(buff);
				buff.Install(role);
			}
		}

		public void UnEquip(Role role)
		{
			foreach (IBuff<Role> roleBuff in _addedBuffsRecord)
				roleBuff.Install(role);

			_addedBuffsRecord.Clear();
		}

		public int this[int index] => Property[index];

		public override void Save(IArchiveWriter writer)
		{
			writer.Write(Antique);
			writer.Write(TypeCode);
			writer.Write(EquipCode);
			this.SaveShown(writer);
			for (int i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
				writer.Write(Property[i]);
			base.Save(writer);
		}

		public void Load(IArchiveReader reader)
		{
			Antique = reader.ReadInt32();
			TypeCode = reader.ReadByte();
			EquipCode = reader.ReadByte();
			this.LoadShown(reader);
			for (int i = 0; i < SharedConsts.EquipmentPropertyCount; i++)
				Property[i] = reader.ReadInt32();
			base.Load(reader, this);
		}

		public void AddRoleBuff(IBuffFactory<Role> factory)
		{
			_roleBuffFactories.Add(factory);
		}

		public void RemoveRoleBuff(IBuffFactory<Role> factory)
		{
			_roleBuffFactories.Remove(factory);
		}

		public void SetContent(Transform transform)
		{
			transform.AddItemHeader(this);
			transform.AddEquipmentProperty(this);
			transform.AddListShown("特效", Values);
		}
	}
}