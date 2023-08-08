#region

using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
	[Registry(DependScope = typeof(GameScope))]
	public class WorldHexMapManager :  IHexMapManager
	{
		private readonly WorldGlobalService _globalService;
		private WorldCell[] _checkers;
		public WorldHexMapManager(
			WorldGlobalService globalService)
		{
			_globalService = globalService;
		}

		public Transform GetFeature(HexCell cell)
		{
			WorldCell cellMessage = (WorldCell)cell.Message;
			if (cellMessage is null) return null;

			int type = cellMessage.GetResourceType();
			int level = cellMessage.GetResourceLevel(type);
			if (level < 0) return null;

			List<PrefabContainer<Transform>> list = _globalService.ResourceFeatures[type, level];
			return list.RandomPick()?.Value;
		}

		public Transform GetSpecial(HexCell cell)
		{
			WorldCell cellMessage = (WorldCell)cell.Message;
			if (cellMessage is null) return null;

			int index = cellMessage.SpecialIndex;
			return index < 0 ? null : _globalService.Features[index]?.Value;
		}


		public int GetMoveCost( HexCell from, HexCell to, HexDirection direction)
		{
			return from.CostTo(to, direction);
		}

		public int GetSpeed(HexUnit unit)
		{
			Role role = GameScope.Instance.Role.Get(unit.Id);
			return role?.GetSpeed() ?? 1;
		}

		public CellBase InitMessage(int x, int z,HexGrid grid)
		{
			return new WorldCell();
		}

		public CellBase GetMessage(HexCell cell)
		{
			if (!cell) return null;

			return cell.Index < 0 ? null : _checkers[cell.Index];
		}
	}
}