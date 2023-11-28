using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffFunction
{
    public class TimeRecorder : BuffRecorderBase
    {
        protected int TimeRecord { get; set; }

        public override void InstallFrom(BattleRole target)
        {
            TimeRecord = Context.TotalTime;
            Context.RoundBeginAction += CalculateRemove;
        }

        public override void UnInstallFrom(BattleRole target)
        {
            Context.RoundBeginAction -= CalculateRemove;
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"(剩余{Remain}时序)");
        }


        protected override void CalculateRemove()
        {
            Buff.BuffRecord -= (Context.TotalTime - TimeRecord) * RemoveValue;
            if (TimeRecord <= 0 && Buff.Target is not null)
            {
                Buff.Target.Buffs.UnInstall(Buff.Id);
                return;
            }

            TimeRecord = Context.TotalTime;
        }
    }
}