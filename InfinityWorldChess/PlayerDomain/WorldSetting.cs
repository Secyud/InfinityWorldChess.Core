#region

using Secyud.Ugf.UgfHexMapGenerator;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
	public class WorldSetting 
	{
		public int WorldSizeX { get; set; } = 12;

		public int WorldSizeZ { get; set; } = 12;
		public int Seed { get; set; }
		
		public int MapId { get; set; }
	}
}