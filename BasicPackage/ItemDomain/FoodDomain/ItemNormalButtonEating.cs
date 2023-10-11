#region

using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.GameMenuDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public class ItemNormalButtonEating : ButtonDescriptor<IItem>
	{
		public override string ShowName => "食用";

		public override void Invoke()
		{
			IEdible edible = Target as IEdible;
			edible?.Eating(GameScope.Instance.Role.MainOperationRole);
			U.Get<GameMenuTabService>().RefreshCurrentTab();
		}

		public override bool Visible(IItem target)
		{
			return target is IEdible;
		}

		public override bool Visible()
		{
			return GameScope.Instance.Role.IsPlayerView();
		}
	}
}