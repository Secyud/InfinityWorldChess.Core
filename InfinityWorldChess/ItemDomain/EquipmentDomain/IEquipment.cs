#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
	public interface IEquipment : IItem, IEquippable<Role>
	{
		public int Antique { get; set; }

		public byte TypeCode { get; }
		public byte Location { get; }

		public int[] Property { get; }
	}
}