using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// be careful, time should be a large number.
    /// </summary>
    public class RoundTrigger : ActionableTrigger
    {
        [field: S(0)] public int Time { get; set; }

        protected float TimeRecord { get; set; }

        public override void Install(BattleRole target)
        {
            TimeRecord = Context.TotalTime;
            Context.RoundBeginAction += CalculateEffect;
        }

        public override void UnInstall(BattleRole target)
        {
            Context.RoundBeginAction -= CalculateEffect;
        }

        private void CalculateEffect()
        {
            float currentTime = Context.TotalTime;
            while (currentTime > TimeRecord + Time)
            {
                Active();
                TimeRecord += Time;
            }
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"每{Time}时序触发。");

            base.SetContent(transform);
        }
    }
}