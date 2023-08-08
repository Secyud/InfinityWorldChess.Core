#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;

#endregion

namespace InfinityWorldChess
{
	public static class IwcExtension
	{
		public static bool CanSet(this IEquipment equipment, byte location)
		{
			return equipment.TypeCode == location;
		}
	}
}