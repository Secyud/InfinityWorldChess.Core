#region

using Secyud.Ugf.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public class ItemNormalButtonEatingInBattle : ButtonRegistration<IItem>
	{
		public override string ShowName => "食用";

		public override void Trigger()
		{
			(Target as IEdibleInBattle)!.EatingInBattle();
		}

		public override bool Visible(IItem target)
		{
			return target is IEdibleInBattle;
		}
	}
}