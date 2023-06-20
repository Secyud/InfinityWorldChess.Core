using Secyud.Ugf.TableComponents;

namespace InfinityWorldChess.RoleDomain.TableComponents
{
	public class RoleSelectTableHelper:SelectableTableHelper<Role, RoleAvatarCell, RoleTf>
	{

		protected override void SetCell(RoleAvatarCell cell, Role item)
		{
			cell.OnInitialize(item.Basic);
		}
	}
}