#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using System.Linq;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
	public class EquipmentForm : MonoBehaviour
	{
		[SerializeField] private NormalCell[] EquipmentCells;

		private Role _role;

		public void OnEquipmentCellClick(int index)
		{
			byte i = (byte)index;
			if (_role.Equipment[i] is null)
			{
				U.Factory.Application.DependencyManager.GetScope<GlobalScope>().OnItemSelectionOpen(
					_role.Item.Where(
						u => u is IEquipment e &&
							(0b1 << index & e.EquipCode) > 0
					).ToList(),
					e => SetEquipment(e as IEquipment, i)
				);
			}
			else
			{
				_role.SetEquipment(null, i);
				RefreshEquipment();
			}
		}

		private void SetEquipment(IEquipment equipment, byte index)
		{
			_role.SetEquipment(equipment, index);
			RefreshEquipment();
		}

		public void RefreshEquipment()
		{
			IEquipment equipmentTmp = null;
			for (byte i = 0; i < SharedConsts.MaxBodyPartsCount; i++)
			{
				IEquipment equipment = _role.Equipment[i];
				if (equipment is null || equipmentTmp == equipment)
				{
					EquipmentCells[i].OnInitialize(null);
					EquipmentCells[i].SetFloating(null);
				}
				else
				{
					EquipmentCells[i].OnInitialize(equipment);
					EquipmentCells[i].SetFloating(equipment);
				}

				equipmentTmp = equipment;
			}
		}

		public void OnInitialize(Role role)
		{
			_role = role;

			RefreshEquipment();
		}
	}
}