using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public class RoundTrigger : BuffTriggerBase
    {
        [field:S] private int Time { get; set; }

        private float _timeRecord;
        public override string ShowDescription => $"每{Time}时序触发," + Effect.ShowDescription;

        public override void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            base.Install(target, buff);
            BattleScope.Instance.Battle.RoundBeginAction += EffectCheck;
            _timeRecord = BattleScope.Instance.Battle.TotalTime;
        }

        public override void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            base.UnInstall(target, buff);
            BattleScope.Instance.Battle.RoundBeginAction -= EffectCheck;
        }

        private void EffectCheck()
        {
            float currentTime = BattleScope.Instance.Battle.TotalTime;
            while (currentTime > _timeRecord + Time)
            {
                Effect.Active();
                _timeRecord += Time;
            }
        }
    }
}