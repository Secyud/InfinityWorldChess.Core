#region

using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public class ItemNormalButtonEatingInBattle : ButtonDescriptor<IItem>
	{
		public override string Name => "食用";

		public override void Invoke()
		{
			(Target as IEdibleInBattle)!.EatingInBattle(BattleScope.Instance.Context.Unit);
		}

		public override bool Visible(IItem target)
		{
			return target is IEdibleInBattle;
		}
	}
}