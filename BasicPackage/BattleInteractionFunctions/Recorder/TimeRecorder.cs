using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [ID("897C91CF-C1B0-D980-8D74-91A8B3691A53")]
    public class TimeRecorder : BuffRecorderBase
    {
        protected int TimeRecord { get; set; }

        public override void InstallFrom(BattleUnit target)
        {
            TimeRecord = Context.TotalTime;
            Context.RoundBeginAction += CalculateRemove;
        }

        public override void UnInstallFrom(BattleUnit target)
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