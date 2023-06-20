#region

using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public class ItemNormalButtonEating : ButtonRegistration<IItem>
	{
		public override string ShowName => "食用";

		public override void Trigger()
		{
			(Target as IEdible)!.Eating();
		}

		public override bool Visible(IItem target)
		{
			return target is IEdible;
		}

		public override bool Visible()
		{
			return GameScope.RoleGameContext.IsPlayerView();
		}
	}
}