#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleRoleDomain;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public class ItemNormalButtonEatingInBattle : ButtonDescriptor<IItem>
	{
		public override string ShowName => "食用";

		public override void Trigger()
		{
			(Target as IEdibleInBattle)!.EatingInBattle(BattleScope.Instance.Get<RoleObservedService>().Role);
		}

		public override bool Visible(IItem target)
		{
			return target is IEdibleInBattle;
		}
	}
}