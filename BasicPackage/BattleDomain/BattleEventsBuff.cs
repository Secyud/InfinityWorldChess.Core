#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	public sealed class BattleEventsBuff : IBuff<BattleRole>
	{
		public ActionableContainer<SkillInteraction> PrepareLaunch { get; } = new();

		public ActionableContainer<SkillInteraction> PrepareReceive { get; } = new();

		public ActionableContainer<SkillInteraction> LaunchCallback { get; } = new();

		public ActionableContainer<SkillInteraction> ReceiveCallback { get; } = new();

		public void Install(BattleRole target)
		{
			PrepareLaunch.Clear();
			PrepareReceive.Clear();
			LaunchCallback.Clear();
			ReceiveCallback.Clear();
		}

		public void UnInstall(BattleRole target)
		{
		}

		public void Overlay(IBuff<BattleRole> finishBuff)
		{
			throw new UgfException($"{nameof(BattleEventsBuff)} cannot be overlapped!");
		}
	}
}