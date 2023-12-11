using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [ID("5828303A-2706-9A32-08E7-992C5DD65E01")]
    public class TurnRecorder : BuffRecorderBase
    {
        public override void InstallFrom(BattleUnit target)
        {
            Context.RoundBeginAction += CalculateRemove;
        }

        public override void UnInstallFrom(BattleUnit target)
        {
            Context.RoundBeginAction -= CalculateRemove;
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"(持续{Remain}回合)");
        }

        protected override void CalculateRemove()
        {
            if (Buff.Target == Context.Unit)
            {
                base.CalculateRemove();
            }
        }
    }
}