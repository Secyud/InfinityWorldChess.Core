#region

using System.Linq;
using UnityEngine;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public class WorldSettingEditor : MonoBehaviour
	{
		private WorldSetting _worldSetting;

		public void SetSeed(string b)
		{
			_worldSetting.Seed =
				b.Aggregate(0, (current, t) => (current << 2) + t);
		}

		public void OnInitialize(WorldSetting worldSetting)
		{
			_worldSetting = worldSetting;
		}
	}
}