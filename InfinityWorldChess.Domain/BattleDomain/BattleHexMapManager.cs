#region

using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using Secyud.Ugf.AssetComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    public class BattleHexMapManager : IBattleHexMapManager
    {
        private readonly BattleGlobalService _globalService;

        public BattleHexMapManager(BattleGlobalService globalService)
        {
            _globalService = globalService;
        }

        public Transform GetFeature(HexCell cell)
        {
            BattleCell cellMessage = cell.Get<BattleCell>();
            if (cellMessage is null) return null;

            int type = cellMessage.ResourceType;
            int level = cellMessage.ResourceLevel;
            if (level < 0 || type < 0) return null;

            List<PrefabContainer<Transform>> list = _globalService.Features[type, level];
            return list.RandomPick()?.Value;
        }

        public Transform GetSpecial(HexCell cell)
        {
            BattleCell cellMessage  = cell.Get<BattleCell>();
            if (cellMessage is null) return null;

            int index = cellMessage.SpecialIndex;
            return index < 0 ? null : _globalService.SpecialFeatures[index]?.Value;
        }

        public int GetMoveCost(HexCell from, HexCell to, HexDirection direction)
        {
            return from.CostTo(to, direction);
        }

        public int GetSpeed(HexUnit unit)
        {
            BattleRole role = BattleScope.Instance.GetChess(unit);
            if (role is null) return 1;

            return role.GetSpeed();
        }

        public CellBase InitMessage(int x, int z, HexGrid grid)
        {
            return new BattleCell();
        }
    }
}