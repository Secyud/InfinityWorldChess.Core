#region

using InfinityWorldChess.GameDomain;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class RoleMessageButtonDescriptor : ButtonDescriptor<Role>
	{
		public override void Trigger()
		{
			GameScope.OpenGameMenu();
		}

		public override string ShowName => "详情";

		public override bool Visible(Role target) => true;
	}
}