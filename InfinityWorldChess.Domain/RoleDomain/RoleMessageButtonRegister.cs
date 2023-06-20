#region

using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.ButtonComponents;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class RoleMessageButtonRegistration : ButtonRegistration<Role>
	{
		public override void Trigger()
		{
			GameScope.OnRoleMessageCreation(Target, 0);
		}

		public override string ShowName => "详情";

		public override bool Visible(Role target) => true;
	}
}