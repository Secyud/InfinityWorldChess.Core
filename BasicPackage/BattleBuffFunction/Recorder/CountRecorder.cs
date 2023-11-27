using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffFunction
{
    public class CountRecorder : BuffRecorderBase
    {
        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"(剩余{Remain}次)");
        }

        public override void InstallFrom(BattleRole target)
        {
            base.InstallFrom(target);
            if (Origin.Effect is ITriggerable trigger)
            {
                trigger.ExtraActions += CalculateRemove;
            }
        }

        public override void UnInstallFrom(BattleRole target)
        {
            base.UnInstallFrom(target);
            if (Origin.Effect is ITriggerable trigger)
            {
                trigger.ExtraActions -= CalculateRemove;
            }
        }
    }
}