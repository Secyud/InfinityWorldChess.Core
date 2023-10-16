#region

using Secyud.Ugf.UgfHexMapGenerator;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
	public class WorldSetting : HexMapGeneratorParameter
	{
		public int WorldSizeX { get; set; } = 12;

		public int WorldSizeZ { get; set; } = 12;
	}
}