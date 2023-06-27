#region

using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
	public class EquipmentButtonRegistration : ButtonRegistration<IItem>
	{
		public override void Trigger()
		{
			U.Factory.Application.DependencyManager.GetScope<GlobalScope>()
				.OnBodySelectionOpen((Target as IEquipment)!.EquipCode, SetEquipment);
		}

		public void SetEquipment(int body)
		{
			IEquipment equipment = Target as IEquipment;
			GameScope scope = U.Factory.Application.DependencyManager.GetScope<GameScope>();
			Role role = scope.Get<RoleGameContext>().MainOperationRole;
			if (body < 0)
				role.TryRemoveEquipment(equipment);
			else
				role.SetEquipment(equipment, (byte)body);
			GameScope.RefreshRoleMessageMenu();
		}

		public override string ShowName => "装备";

		public override bool Visible(IItem target) => target is IEquipment;

		public override bool Visible()
		{
			return GameScope.Instance.Role.IsPlayerView();
		}
	}
}