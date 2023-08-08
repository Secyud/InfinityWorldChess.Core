using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleRoleDomain;
using JetBrains.Annotations;
using Secyud.Ugf;

namespace InfinityWorldChess.BasicBundle.BattleBuffs.Recorders
{
	public class TurnRecorder
	{
		private readonly Type _buffType;
		private BattleRole _target;

		public int TurnFinished { get; set; }

		public string Description => $"(持续{TurnFinished}回合)";

		public TurnRecorder([NotNull] Type buffType)
		{
			_buffType = buffType;
		}

		private void CalculateRemove()
		{
			TurnFinished -= 1;
			if (TurnFinished <= 0 && _target == BattleScope.Instance.Get<RoleRefreshService>().Role)
				_target.UnInstall(_buffType);
		}

		public void Install(BattleRole target)
		{
			_target = target;
			BattleScope.Instance.Context.RoundBeginAction += CalculateRemove;
		}

		public void UnInstall()
		{
			BattleScope.Instance.Context.RoundBeginAction -= CalculateRemove;
		}

		public void Overlay(TurnRecorder recorder)
		{
			if (recorder == this)
				return;

			recorder.TurnFinished += TurnFinished;
		}
	}
}