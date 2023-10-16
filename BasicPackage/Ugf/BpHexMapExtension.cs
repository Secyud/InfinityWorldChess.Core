#region

using Secyud.Ugf.HexMap;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.Ugf
{
	public static class BpHexMapExtension
	{
		public static void TryAdd(this List<HexCell> cells, HexGrid grid, HexCoordinates coordinates)
		{
			HexCell cell = grid.GetCell(coordinates);
			if (cell) cells.Add(cell);
		}
	}
}