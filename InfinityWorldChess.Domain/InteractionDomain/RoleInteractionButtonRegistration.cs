﻿#region

using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.ButtonComponents;

#endregion

namespace InfinityWorldChess.InteractionDomain
{
	public class RoleInteractionButtonRegistration : ButtonRegistration<Role>
	{
		private IInteractionGlobalService _interactionGlobalService;

		public override void Trigger()
		{
			_interactionGlobalService ??= Og.DefaultProvider.Get<IInteractionGlobalService>();
			Role role = GameScope.PlayerGameContext.Role;
			Og.ScopeFactory.CreateScope<InteractionScope>().OnCreation(
				_interactionGlobalService.GenerateFreeInteraction(role,Target));
		}

		public override string ShowName => "交互";

		public override bool Visible(Role target) => true;
	}
}