#region

using System.Collections.Generic;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	/// <summary>
	/// battle events buff record all events for battle role.
	/// buff can effect by add event to this and loss effect by remove event.
	/// </summary>
	public sealed class BattleEvents : BattleRecordProperty
	{
		public HashSet<IActionable<BattleInteraction>> PrepareLaunch { get; } = new();
		public HashSet<IActionable<BattleInteraction>> PrepareReceive { get; } = new();
		public HashSet<IActionable<BattleInteraction>> LaunchCallback { get; } = new();
		public HashSet<IActionable<BattleInteraction>> ReceiveCallback { get; } = new();

		public override void InstallFrom(BattleRole target)
		{
			PrepareLaunch.Clear();
			PrepareReceive.Clear();
			LaunchCallback.Clear();
			ReceiveCallback.Clear();
		}
	}
}