#region

using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf.ButtonComponents;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class RoleWorldCheckerBf : ButtonFunctionBase<Role>
	{
		public RoleWorldCheckerBf()
		{
			RegisterList(new RoleMessageButtonRegistration(),
				new RoleInteractionButtonRegistration());
		}
	}
}