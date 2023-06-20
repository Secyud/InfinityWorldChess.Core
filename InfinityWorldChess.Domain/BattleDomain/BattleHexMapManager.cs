#region

using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	public class BattleHexMapManager : IScoped, IBattleHexMapManager
	{
		private readonly BattleContext _context;
		private readonly BattleGlobalService _globalService;

		public BattleHexMapManager(BattleGlobalService globalService, BattleContext context)
		{
			_globalService = globalService;
			_context = context;
		}

		public Transform GetFeature(HexCell cell)
		{
			BattleChecker cellMessage = _context.GetChecker(cell);
			if (cellMessage is null) return null;

			int type = cellMessage.ResourceType;
			int level = cellMessage.ResourceLevel;
			if (level < 0 || type < 0) return null;

			List<PrefabContainer<Transform>> list = _globalService.Features[type, level];
			return list.RandomPick()?.Value;
		}

		public Transform GetSpecialFeature(HexCell cell)
		{
			BattleChecker cellMessage = _context.GetChecker(cell);
			if (cellMessage is null) return null;

			int index = cellMessage.SpecialIndex;
			return index < 0 ? null : _globalService.SpecialFeatures[index]?.Value;
		}

		public int GetMoveCost(HexUnit unit, HexCell from, HexCell to, HexDirection direction)
		{
			return from.CostTo(to, direction);
		}

		public int GetSpeed(HexUnit unit)
		{
			IBattleChess role = _context.GetChess(unit);
			if (role is null) return 1;

			return role.GetSpeed();
		}
	}
}