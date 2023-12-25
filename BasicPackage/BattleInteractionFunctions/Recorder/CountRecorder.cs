using System.Runtime.InteropServices;
using InfinityWorldChess.BattleBuffDomain;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [Guid("F40D4732-5A6C-0A9B-C5FF-48278BDC2A07")]
    public class CountRecorder : BuffRecorderBase
    {
        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"(剩余{Remain}次)");
        }

        public override void InstallFrom(BattleUnit target)
        {
            base.InstallFrom(target);
            if (Buff is BattleUnitBuff { Effect: ITriggerable trigger })
            {
                trigger.ExtraActions += CalculateRemove;
            }
        }

        public override void UnInstallFrom(BattleUnit target)
        {
            base.UnInstallFrom(target);
            if (Buff is BattleUnitBuff { Effect: ITriggerable trigger })
            {
                trigger.ExtraActions -= CalculateRemove;
            }
        }
    }
}