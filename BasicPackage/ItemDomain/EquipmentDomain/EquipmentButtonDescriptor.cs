#region

using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.GameMenuDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
	public class EquipmentButtonDescriptor : ButtonDescriptor<IItem>
	{
		public override void Trigger()
		{
			IEquipment equipment = Target as IEquipment;
			GameScope scope = U.M.GetScope<GameScope>();
			Role role = scope.Get<RoleGameContext>().MainOperationRole;
			
			
			
			role.SetEquipment(equipment);
			U.Get<GameMenuTabService>().RefreshCurrentTab();
		}

		public override string ShowName => "装备";

		public override bool Visible(IItem target) => target is IEquipment;

		public override bool Visible()
		{
			return GameScope.Instance.Role.IsPlayerView();
		}
	}
}