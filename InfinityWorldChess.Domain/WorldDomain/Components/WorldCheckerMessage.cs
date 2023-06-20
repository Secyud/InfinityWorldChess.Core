#region

using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public class WorldCheckerMessage : WorldCheckerInitialize
	{
		[SerializeField] private SText Position;
		[SerializeField] private SText[] Resources;


		public override void OnInitialize(WorldChecker message)
		{
			Position.Set(message.Cell.Coordinates.ToString());
			Resources.Set(
				message.Stone.ToString(),
				message.Tree.ToString(),
				message.Farm.ToString()
			);
		}
	}
}