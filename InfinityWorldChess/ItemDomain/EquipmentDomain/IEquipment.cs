#region

using InfinityWorldChess.GlobalDomain;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
	public interface IEquipment : IItem, ICanBeEquipped
	{
		public int Antique { get; set; }

		public byte TypeCode { get; }

		public int[] Property { get; }
	}
}