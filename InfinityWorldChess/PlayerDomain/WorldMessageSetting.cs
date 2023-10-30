#region

using Secyud.Ugf.UgfHexMapGenerator;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
	public class WorldMessageSetting 
	{
		public int Seed { get; set; }

		public string WorldName { get; set; } = "default";
	}
}