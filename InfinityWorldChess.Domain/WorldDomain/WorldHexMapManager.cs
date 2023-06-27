#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	[Registry()]
	public class WorldHexMapManager :  IHexMapManager
	{
		private readonly WorldGlobalService _globalService;

		public WorldHexMapManager(
			WorldGlobalService globalService)
		{
			_globalService = globalService;
		}


		public Transform GetFeature(HexCell cell)
		{
			WorldChecker cellMessage = GameScope.Instance.World.GetChecker(cell);
			if (cellMessage is null) return null;

			int type = cellMessage.GetResourceType();
			int level = cellMessage.GetResourceLevel(type);
			if (level < 0) return null;

			List<PrefabContainer<Transform>> list = _globalService.ResourceFeatures[type, level];
			return list.RandomPick()?.Value;
		}

		public Transform GetSpecialFeature(HexCell cell)
		{
			WorldChecker cellMessage = GameScope.Instance.World.GetChecker(cell);
			if (cellMessage is null) return null;

			int index = cellMessage.SpecialIndex;
			return index < 0 ? null : _globalService.Features[index]?.Value;
		}

		public int GetMoveCost(HexUnit unit, HexCell from, HexCell to, HexDirection direction)
		{
			return from.CostTo(to, direction);
		}

		public int GetSpeed(HexUnit unit)
		{
			Role role = GameScope.Instance.Role.Get(unit.Id);
			return role?.GetSpeed() ?? 1;
		}
	}
}