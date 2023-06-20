#region

using InfinityWorldChess.RoleDomain;

#endregion

namespace InfinityWorldChess.GlobalDomain
{
	public interface ICanBeEquipped
	{
		void Equip(Role role);

		void UnEquip(Role role);
	}
}