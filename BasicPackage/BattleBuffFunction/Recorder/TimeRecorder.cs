using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffFunction
{
    public class TimeRecorder : BuffRecorderBase
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
            Origin.BuffRecord -= (Context.TotalTime - TimeRecord) * RemoveValue;
            if (TimeRecord <= 0 && Origin.Target is not null)
            {
                Origin.Target.Buffs.UnInstall(Origin.Id);
                return;
            }

            TimeRecord = Context.TotalTime;
        }
    }
}