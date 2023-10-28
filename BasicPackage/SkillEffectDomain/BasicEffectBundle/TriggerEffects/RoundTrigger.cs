using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    /// <summary>
    /// be careful, time should be a large number.
    /// </summary>
    public class RoundTrigger : BuffTriggerBase
    {
        [field:S] private int Time { get; set; }

        private float _timeRecord;
        public override string Description => $"每{Time}时序触发," + base.Description;

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
                foreach (ITriggerEffect e in Effects)
                {
                    e.Active();
                }
                _timeRecord += Time;
            }
        }
    }
}