#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.GlobalDomain
{
	public interface ICanBeEquipped:IHasDescription
	{
		void Equip(Role role);

		void UnEquip(Role role);
	}
}