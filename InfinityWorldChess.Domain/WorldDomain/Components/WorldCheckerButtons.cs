#region

using InfinityWorldChess.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public class WorldCheckerButtons : WorldCheckerInitialize
	{
		[SerializeField] private ButtonGroup Group;

		public override void OnInitialize(WorldChecker checker)
		{
			Group.OnInitialize(checker, checker.Buttons);
		}
	}
}