#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public class WorldHexMapManager : IScoped, IHexMapManager
	{
		private readonly WorldGameContext _gameContext;
		private readonly WorldGlobalService _globalService;
		private readonly RoleGameContext _roleGameContext;

		public WorldHexMapManager(
			WorldGlobalService globalService,
			WorldGameContext gameContext,
			RoleGameContext roleGameContext)
		{
			_globalService = globalService;
			_gameContext = gameContext;
			_roleGameContext = roleGameContext;
		}


		public Transform GetFeature(HexCell cell)
		{
			WorldChecker cellMessage = _gameContext.GetChecker(cell);
			if (cellMessage is null) return null;

			int type = cellMessage.GetResourceType();
			int level = cellMessage.GetResourceLevel(type);
			if (level < 0) return null;

			List<PrefabContainer<Transform>> list = _globalService.ResourceFeatures[type, level];
			return list.RandomPick()?.Value;
		}

		public Transform GetSpecialFeature(HexCell cell)
		{
			WorldChecker cellMessage = _gameContext.GetChecker(cell);
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
			Role role = _roleGameContext.Get(unit.Id);
			if (role is null) return 1;

			return role.GetSpeed();
		}
	}
}