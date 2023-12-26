#region

using InfinityWorldChess.GameDomain;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class RoleMessageButtonDescriptor : ButtonDescriptor<Role>
	{
		public override void Invoke()
		{
			GameScope.Instance.OpenGameMenu();
		}

		public override string Name => "详情";

		public override bool Visible(Role target) => true;
	}
}