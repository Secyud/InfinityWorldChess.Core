using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public class TimeRecorder : IBuffRecorder
    {
        private BattleRole _role;
        private IBuff<BattleRole> _buff;

        public float TimeFinished { get; set; }
        public float TimeRecord { get; private set; }

        public string ShowDescription => $"(持续{TimeFinished:N0}时序)";

        private void CalculateRemove()
        {
            TimeFinished -= BattleScope.Instance.Context.TotalTime - TimeRecord;
            if (TimeRecord <= 0 && _role is not null)
            {
                _role.UnInstall(_buff.Id);
                return;
            }

            TimeRecord = BattleScope.Instance.Context.TotalTime;
        }

        public void Overlay(TimeRecorder recorder)
        {
            if (recorder == this)
                return;
            recorder.TimeFinished += TimeFinished;
        }

        public void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            _role = target;
            _buff = buff;
            BattleScope.Instance.Context.RoundBeginAction += CalculateRemove;
            TimeRecord = BattleScope.Instance.Context.TotalTime;
        }

        public void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            BattleScope.Instance.Context.RoundBeginAction -= CalculateRemove;
        }

        public void Overlay(IBuffRecorder thisRecorder, IBuff<BattleRole> buff)
        {
        }
    }
}