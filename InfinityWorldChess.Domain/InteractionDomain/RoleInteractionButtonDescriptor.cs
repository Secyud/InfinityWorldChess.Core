#region

using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.InteractionDomain
{
	public class RoleInteractionButtonDescriptor : ButtonDescriptor<Role>
	{
		private IInteractionGlobalService _interactionGlobalService;

		public override void Trigger()
		{
			_interactionGlobalService ??= U.Get<IInteractionGlobalService>();
			Role role = GameScope.Instance.Player.Role;
			U.Factory.Application.DependencyManager.CreateScope<InteractionScope>().OnCreation(
				_interactionGlobalService.GenerateFreeInteraction(role,Target));
		}

		public override string ShowName => "交互";

		public override bool Visible(Role target) => true;
	}
}