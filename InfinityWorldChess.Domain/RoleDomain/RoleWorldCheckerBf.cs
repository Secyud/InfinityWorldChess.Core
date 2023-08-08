#region

using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class RoleWorldCheckerBf : ButtonRegeditBase<Role>
	{
		public RoleWorldCheckerBf()
		{
			RegisterList(new RoleMessageButtonDescriptor(),
				new RoleInteractionButtonDescriptor());
		}
	}
}