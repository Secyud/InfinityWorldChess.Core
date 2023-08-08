using System;
using InfinityWorldChess.BattleDomain;
using JetBrains.Annotations;

namespace InfinityWorldChess.BasicBundle.BattleBuffs.Recorders
{
	public class TrigRecorder
	{
		private BattleContext _context;
		private readonly Type _buffType;
		private BattleRole _target;

		public int TrigFinished { get; set; }

		public string Description => $"(持续{TrigFinished}次)";

		public TrigRecorder([NotNull] Type buffType)
		{
			_buffType = buffType;
		}

		public void CalculateRemove()
		{
			TrigFinished -= 1;
			if (TrigFinished <= 0)
				_target.UnInstall(_buffType);
		}

		public void Install(BattleRole target)
		{
			_target = target;
		}

		public void Overlay(TurnRecorder recorder)
		{
			if (recorder.TurnFinished < TrigFinished)
				recorder.TurnFinished = TrigFinished;
		}
	}
}