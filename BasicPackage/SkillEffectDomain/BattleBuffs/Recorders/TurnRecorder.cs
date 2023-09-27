﻿using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleRoleDomain;
using InfinityWorldChess.BuffDomain;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public class TurnRecorder : IBuffRecorder
    {
        private IBuff<BattleRole> _buff;
        private BattleRole _target;
        public int TurnFinished { get; set; }
        public string  ShowDescription => $"(持续{TurnFinished}回合)";

        private void CalculateRemove()
        {
            TurnFinished -= 1;
            if (TurnFinished <= 0 && _target == BattleScope.Instance.Get<RoleObservedService>().Role)
                _target.UnInstall(_buff.Id);
        }
        
        public void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            _target = target;
            BattleScope.Instance.Context.RoundBeginAction += CalculateRemove;
        }

        public void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            BattleScope.Instance.Context.RoundBeginAction -= CalculateRemove;
        }

        public void Overlay(IBuffRecorder thisRecorder, IBuff<BattleRole> buff)
        {
            if (thisRecorder is not TurnRecorder recorder ||
                recorder == this)
                return;

            recorder.TurnFinished += TurnFinished;
        }
    }
}