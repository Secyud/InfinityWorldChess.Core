#region

using Secyud.Ugf.UgfHexMapGenerator;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
	public class WorldSetting 
	{
		public int Seed { get; set; }

		public string PlayName { get; set; } = "default";
	}
}