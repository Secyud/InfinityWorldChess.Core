#region

using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;

#endregion

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain
{
	public class SkillRange : ISkillRange, IObjectAccessor<HexCell[]>
	{
		public SkillRange(params HexCell[] cells)
		{
			Value = cells.ToArray();
		}

		public HexCell[] Value { get; }

		public static SkillRange GetFixedRange(params HexCell[] cells)
		{
			return new SkillRange(cells);
		}

		public static SkillRange Circle(byte start, byte end,
			HexCoordinates center,bool includeUnit = true)
		{
			return GetArcRange(start, end, center, HexDirection.Ne, 6,includeUnit);
		}

		public static SkillRange WideHalfCircle(byte start, byte end, HexCoordinates center, HexDirection direction)
		{
			return GetArcRange(
				start, end, center,
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
		public static SkillRange HalfCircle(byte start, byte end, HexCoordinates center, HexDirection direction)
		{
			return GetArcRange(start, end, center,
				(HexDirection)(((int)direction + 5) % 6), 3
			);
		}

		public static SkillRange ObtuseTriangle(byte start, byte end, HexCoordinates center, HexDirection direction)
		{
			return GetArcRange(start, end, center,
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
		public static SkillRange AcuteTriangle(byte start, byte end, HexCoordinates center, HexDirection direction)
		{
			return GetArcRange(start, end, center, direction, 1);
		}

		public static SkillRange Line(byte start, byte end, HexCoordinates center, HexDirection direction)
		{
			return GetArcRange(start, end, center, direction, 0);
		}
		
		public static SkillRange GetArcRange(
			byte startDistance, byte endDistance, HexCoordinates startCoordinate,
			HexDirection startDirection, byte coverRange,bool includeUnit = true)
		{
			HexGrid grid = BattleScope.Instance.Map.Grid;
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

			if (!includeUnit)
			{
				for (int i = 0; i < cells.Count; )
				{
					if (cells[i].Unit)
					{
						cells.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
			
			return new SkillRange(cells.ToArray());
		}
	}
}