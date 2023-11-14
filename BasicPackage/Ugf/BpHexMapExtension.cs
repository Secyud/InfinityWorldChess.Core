#region

using Secyud.Ugf.HexMap;
using System.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.HexUtilities;

#endregion

namespace InfinityWorldChess.Ugf
{
	public static class BpHexMapExtension
	{
		public static void TryAdd(this List<BattleCell> cells, HexGrid grid, HexCoordinates coordinates)
		{
			BattleCell cell = grid.GetCell(coordinates) as BattleCell;
			if (cell) cells.Add(cell);
		}
	}
}