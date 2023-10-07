#region

using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public class ItemNormalButtonEatingInBattle : ButtonDescriptor<IItem>
	{
		public override string ShowName => "食用";

		public override void Trigger()
		{
			(Target as IEdibleInBattle)!.EatingInBattle(BattleScope.Instance.Context.Role);
		}

		public override bool Visible(IItem target)
		{
			return target is IEdibleInBattle;
		}
	}
}