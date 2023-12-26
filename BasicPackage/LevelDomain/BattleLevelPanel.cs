using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.GameDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.LevelDomain
{
    public class BattleLevelPanel:MonoBehaviour
    {
        [SerializeField] private Table Table;

        private void Awake()
        {
            BattleLevelGlobalContext service = U.Get<BattleLevelGlobalContext>();
            int playerLevel = GameScope.Instance.Player.Role.Basic.Level;
            Table.AutoSetSingleSelectTable<IBattleLevel, BattleLevelSorters, BattleLevelFilters>
                (service.LevelList.Items.Where(u=>u.Level<= playerLevel).ToList(), CreateBattle);
        }

        private static void CreateBattle(IBattleLevel level)
        {
            if (level is not null)
            {
                BattleScope.CreateBattle(level);
            }
        }
    }
}