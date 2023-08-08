﻿using System;
using System.Diagnostics.CodeAnalysis;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.BasicBundle.BattleBuffs.Recorders
{
	public class TimeRecorder
	{
		private BattleContext _context;
		private readonly Type _buffType;
		private BattleRole _target;

		public float TimeFinished { get; set; }
		public float TimeRecord { get; private set; }

		public string Description => $"(持续{TimeFinished:N0}时序)";
		
		public TimeRecorder([NotNull]Type buffType)
		{
			_buffType = buffType;
		}
		
		private void CalculateRemove()
		{
			TimeFinished -= BattleScope.Instance.Context.TotalTime - TimeRecord;
			if (TimeRecord <= 0 && _target is not null)
			{
				_target.UnInstall(_buffType);
				return;
			}
			TimeRecord = BattleScope.Instance.Context.TotalTime;
		}
		
		public  void Install(BattleRole target)
		{
			_target = target;
			BattleScope.Instance.Context.RoundBeginAction += CalculateRemove;
			TimeRecord = BattleScope.Instance.Context.TotalTime;
		}
		

		public void UnInstall()
		{
			BattleScope.Instance.Context.RoundBeginAction -= CalculateRemove;
		}

		public void Overlay(TimeRecorder recorder)
		{
			if (recorder == this )
				return;
			recorder.TimeFinished += TimeFinished;
		}
	}
}