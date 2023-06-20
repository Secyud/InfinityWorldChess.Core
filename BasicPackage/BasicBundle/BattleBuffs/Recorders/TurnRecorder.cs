using System;
using InfinityWorldChess.BattleDomain;
using JetBrains.Annotations;

namespace InfinityWorldChess.BasicBundle.BattleBuffs.Recorders
{
	public class TurnRecorder
	{
		private BattleContext _context;
		private readonly Type _buffType;
		private RoleBattleChess _target;

		public int TurnFinished { get; set; }

		public string Description => $"(持续{TurnFinished}回合)";

		public TurnRecorder([NotNull] Type buffType)
		{
			_buffType = buffType;
		}

		private void CalculateRemove()
		{
			TurnFinished -= 1;
			if (TurnFinished <= 0 && _target == _context.CurrentRole)
				_target.UnInstall(_buffType);
		}

		public void Install(RoleBattleChess target)
		{
			_target = target;
			BattleScope.Context.RoundBeginAction += CalculateRemove;
		}

		public void UnInstall()
		{
			BattleScope.Context.RoundBeginAction -= CalculateRemove;
		}

		public void Overlay(TurnRecorder recorder)
		{
			if (recorder == this)
				return;

			recorder.TurnFinished += TurnFinished;
		}
	}
}