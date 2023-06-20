using Secyud.Ugf.TableComponents;

namespace InfinityWorldChess.RoleDomain.TableComponents
{
	public class RoleTableHelper:FunctionalTableHelper<Role, RoleAvatarCell, RoleTf>
	{

		protected override void SetCell(RoleAvatarCell cell, Role item)
		{
			cell.OnInitialize(item.Basic);
		}
	}
}