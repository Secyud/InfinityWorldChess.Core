#region

using Secyud.Ugf.HexMap.Generator;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
	public class WorldSetting : HexMapGeneratorParameter
	{
		public int WorldSizeX { get; set; } = 12;

		public int WorldSizeZ { get; set; } = 12;
	}
}