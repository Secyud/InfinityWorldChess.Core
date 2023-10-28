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
		public override void Invoke()
		{
			IEquipment equipment = Target as IEquipment;
			Role role = GameScope.Instance.Role.MainOperationRole;
			role.SetEquipment(equipment);
			U.Get<GameMenuTabService>().RefreshCurrentTab();
		}

		public override string Name => "装备";

		public override bool Visible(IItem target) => target is IEquipment;

		public override bool Visible()
		{
			return GameScope.Instance.Role.IsPlayerView();
		}
	}
}