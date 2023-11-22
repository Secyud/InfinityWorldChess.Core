using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class TimeRecorder : EffectRecorder
    {
        protected int TimeRecord { get; set; }

        public override void Install(BattleRole target)
        {
            TimeRecord = Context.TotalTime;
            Context.RoundBeginAction += CalculateRemove;
        }

        public override void UnInstall(BattleRole target)
        {
            Context.RoundBeginAction -= CalculateRemove;
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"(剩余{Remain}时序)");
        }


        protected override void CalculateRemove()
        {
            BelongBuff.BuffRecord -= (Context.TotalTime - TimeRecord) * RemoveValue;
            if (TimeRecord <= 0 && BelongBuff.Target is not null)
            {
                BelongBuff.Target.Buff.UnInstall(BelongBuff.Id);
                return;
            }

            TimeRecord = Context.TotalTime;
        }
    }
}