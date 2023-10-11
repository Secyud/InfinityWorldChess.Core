#region

using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.GameMenuDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.BookDomain
{
	public class ItemNormalButtonReading : ButtonDescriptor<IItem>
	{
		public override string ShowName => "研读";

		public override void Invoke()
		{
			IReadable readable = Target as IReadable;
			Role role = GameScope.Instance.Role.MainOperationRole;
			readable?.Reading(role);
			U.Get<GameMenuTabService>().RefreshCurrentTab();
		}

		public override bool Visible(IItem target)
		{
			return target is IReadable;
		}
	}
}