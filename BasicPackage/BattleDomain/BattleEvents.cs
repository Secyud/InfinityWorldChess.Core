#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	/// <summary>
	/// battle events buff record all events for battle role.
	/// buff can effect by add event to this and loss effect by remove event.
	/// </summary>
	public sealed class BattleEvents : BattleRecordProperty
	{
		public ActionableContainer<SkillInteraction> PrepareLaunch { get; } = new();

		public ActionableContainer<SkillInteraction> PrepareReceive { get; } = new();

		public ActionableContainer<SkillInteraction> LaunchCallback { get; } = new();

		public ActionableContainer<SkillInteraction> ReceiveCallback { get; } = new();

		public override void Install(BattleRole target)
		{
			PrepareLaunch.Clear();
			PrepareReceive.Clear();
			LaunchCallback.Clear();
			ReceiveCallback.Clear();
		}
	}
}