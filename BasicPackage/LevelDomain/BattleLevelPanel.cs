using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleTemplates;
using InfinityWorldChess.Ugf;
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

            Table.AutoSetSingleSelectTable<IBattleLevel,
                BattleLevelSorters, BattleLevelFilters>(service.LevelList.Items, CreateBattle);
        }

        private void CreateBattle(IBattleLevel level)
        {
            BattleScope.CreateBattle(level);
        }
    }
}