#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class SkillRange : ISkillRange, IObjectAccessor<HexCell[]>
	{
		private SkillRange(IEnumerable<HexCell> cells)
		{
			Value = cells.ToArray();
		}

		public HexCell[] Value { get; }

		public static SkillRange GetFixedRange(IEnumerable<HexCell> cells)
		{
			return new SkillRange(cells);
		}

		public static SkillRange Circle(int start, int end, HexCell center)
		{
			return GetArcRange(start, end, center.Coordinates, HexDirection.Ne, 5);
		}

		public static SkillRange WideHalfCircle(int start, int end, HexCell center, HexDirection direction)
		{
			return GetArcRange(
				start, end, center.Coordinates,
				(HexDirection)(((int)direction + 4) % 6), 4
			);
		}

		/// <summary>
		/// not useful
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="center"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static SkillRange HalfCircle(int start, int end, HexCell center, HexDirection direction)
		{
			return GetArcRange(
				start, end, center.Coordinates,
				(HexDirection)(((int)direction + 5) % 6), 3
			);
		}

		public static SkillRange WideTriangle(int start, int end, HexCell center, HexDirection direction)
		{
			return GetArcRange(
				start, end, center.Coordinates,
				(HexDirection)(((int)direction + 5) % 6), 2
			);
		}

		/// <summary>
		/// not useful
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="center"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static SkillRange Triangle(int start, int end, HexCell center, HexDirection direction)
		{
			return GetArcRange(start, end, center.Coordinates, direction, 1);
		}

		public static SkillRange Line(int start, int end, HexCell center, HexDirection direction)
		{
			return GetArcRange(start, end, center.Coordinates, direction, 0);
		}

		public static SkillRange Point(HexCell center)
		{
			return new SkillRange(new[] {center});
		}

		public static SkillRange GetArcRange(
			int startDistance, int endDistance, HexCoordinates startCoordinate,
			HexDirection startDirection, int coverRange)
		{
			HexGrid grid = BattleScope.Context.Map.Grid;
			List<HexCell> cells = new();
			HexCoordinates coordinate = startCoordinate;

			for (int i = 0; i < startDistance; i++)
				coordinate += startDirection;

			int rangeDirection = (int)startDirection + 2;

			for (int i = startDistance; i < endDistance; i++)
			{
				HexCoordinates tmp = coordinate;
				for (int j = 0; j < coverRange; j++)
				{
					HexDirection direction = (HexDirection)((rangeDirection + j) % 6);
					for (int k = 0; k < i; k++)
					{
						cells.TryAdd(grid, tmp);
						tmp += direction;
					}
				}

				cells.TryAdd(grid, tmp);
				coordinate += startDirection;
			}

			return new SkillRange(cells);
		}

		public static string GetRangeInfo(byte type, byte start, byte end)
		{
			return type switch
			{
				0 => "中心",
				1 => $"直线 ({start}-{end})",
				2 => $"三角 ({start}-{end})",
				3 => $"半圆 ({start}-{end})",
				4 => $"圆形 ({start}-{end})",
				5 => $"自身三角 ({start}-{end})",
				6 => $"自身半圆 ({start}-{end})",
				7 => $"自身圆形 ({start}-{end})",
				_ => "中心"
			};
		}
	}
}