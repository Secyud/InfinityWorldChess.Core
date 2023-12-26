using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.ItemDomain
{
    public class EatInBattleComponent:MonoBehaviour
    {
        public void OpenSelectPanel()
        {
            if (BattleScope.Instance is not null)
            {
                GlobalDomain.GlobalScope.Instance.OpenSelect()
                    .AutoSetSingleSelectTable<IItem,ItemSorters,ItemFilters>(
                        BattleScope.Instance.Context.Unit.Role.Item.All()
                            .Where(u=>u is IEdibleInBattle).ToList(),EatItem);
            }
        }

        private static void EatItem(IItem item)
        {
            ((IEdibleInBattle)item).EatingInBattle(
                    BattleScope.Instance.Context.Unit);
        }
    }
}