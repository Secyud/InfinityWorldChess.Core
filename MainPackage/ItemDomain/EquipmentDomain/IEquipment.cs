#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
	public interface IEquipment : IItem, IInstallable<Role>,IAttachProperty
	{
		public byte TypeCode { get; }
		
		public byte Location { get; }
	}
}