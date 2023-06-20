#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	public sealed class BattleEventsBuff : IBuff<RoleBattleChess>
	{
		public ActionableContainer<SkillInteraction> PrepareLaunch { get; } = new();

		public ActionableContainer<SkillInteraction> PrepareReceive { get; } = new();

		public ActionableContainer<SkillInteraction> LaunchCallback { get; } = new();

		public ActionableContainer<SkillInteraction> ReceiveCallback { get; } = new();

		public void Install(RoleBattleChess target)
		{
			PrepareLaunch.Clear();
			PrepareReceive.Clear();
			LaunchCallback.Clear();
			ReceiveCallback.Clear();
		}

		public void UnInstall(RoleBattleChess target)
		{
		}

		public void Overlay(IBuff<RoleBattleChess> finishBuff)
		{
			throw new UgfException($"{nameof(BattleEventsBuff)} cannot be overlapped!");
		}
	}
}