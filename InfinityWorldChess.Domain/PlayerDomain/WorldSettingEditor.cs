#region

using System.Linq;
using Secyud.Ugf.EditorComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
	public class WorldSettingEditor :  EditorBase<WorldSetting>
	{
		[SerializeField] private EditorEvent<string> SeedText;
		public void SetSeed(string b)
		{
			Property.Seed = b.Aggregate(0, (current, t) => (current << 2) + t);
			SeedText.Invoke(b);
		}
	}
}