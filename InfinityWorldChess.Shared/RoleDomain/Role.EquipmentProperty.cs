#region

using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using Secyud.Ugf;
using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		public EquipmentProperty Equipment { get; } = new();

		public void SetEquipment(IEquipment equipment, byte location)
		{
			TryRemoveEquipment(equipment);
			Equipment[location, this] = equipment;
		}

		public void TryRemoveEquipment(IEquipment equipment)
		{
			if (equipment is null)
				return;

			for (byte i = 0; i < SharedConsts.MaxBodyPartsCount; i++)
			{
				if (Equipment[i] == equipment)
					Equipment[i, this] = null;
			}
		}


		public void AutoEquipEquipment()
		{
			IRoleAiService ai = U.Get<IRoleAiService>();
			if (ai is not null)
				ai.AutoEquipFormSkill(this);
			else
			{
				List<IEquipment> equipments = Item.TryFindCast<IItem, IEquipment>();
				Equipment.AutoEquip(equipments, this);
			}
		}

		public class EquipmentProperty
		{
			private readonly IEquipment[] _equipments = new IEquipment[SharedConsts.MaxBodyPartsCount];
			public IEquipment this[byte location] => _equipments[location];
			public IEquipment this[BodyType location] => _equipments[(byte)location];

			public IEquipment this[byte location, Role role]
			{
				set
				{
					IEquipment current = _equipments[location];
					if (current is not null)
					{
						current.UnEquip(role);
						_equipments[location] = null;
					}

					if (value is null || !value.CanSet(location))
						return;

					_equipments[location] = value;
					value.Equip(role);
				}
			}

			public void Save(IArchiveWriter writer)
			{
				for (int i = 0; i < SharedConsts.MaxBodyPartsCount; i++)
				{
					if (_equipments[i] is null)
					{
						writer.Write(false);
						continue;
					}

					if (i > 0 && _equipments[i] == _equipments[i - 1])
					{
						writer.Write(false);
						continue;
					}

					writer.Write(true);
					writer.Write(_equipments[i].SaveIndex);
				}
			}

			public void Load(IArchiveReader reader, Role role)
			{
				for (byte i = 0; i < SharedConsts.MaxBodyPartsCount; i++)
				{
					this[i, role] = null;
					bool b = reader.ReadBoolean();
					if (!b) continue;

					this[i, role] = role.Item[reader.ReadInt32()] as IEquipment;
				}
			}

			internal void AutoEquip(List<IEquipment> equipments, Role role)
			{
				IEquipment[] tmp = new IEquipment[SharedConsts.MaxBodyPartsCount];

				foreach (IEquipment equipment in equipments)
					for (byte i = 0; i < SharedConsts.MaxBodyPartsCount; i++)
						if (equipment.CanSet(i) && (tmp[i] == null || tmp[i].Score < equipment.Score))
						{
							tmp[i] = equipment;
							break;
						}
				for (byte i = 0; i < SharedConsts.MaxBodyPartsCount; i++)
					this[i, role] = tmp[i];
			}
		}
	}
}