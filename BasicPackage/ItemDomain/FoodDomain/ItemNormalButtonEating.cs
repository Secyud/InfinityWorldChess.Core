#region

using InfinityWorldChess.GameDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public class ItemNormalButtonEating : ButtonDescriptor<IItem>
	{
		public override string ShowName => "食用";

		public override void Trigger()
		{
			(Target as IEdible)!.Eating(GameScope.Instance.Player.Role);
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