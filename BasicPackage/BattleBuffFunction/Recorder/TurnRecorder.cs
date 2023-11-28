using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffFunction
{
    public class TurnRecorder : BuffRecorderBase
    {
        public override void InstallFrom(BattleRole target)
        {
            Context.RoundBeginAction += CalculateRemove;
        }

        public override void UnInstallFrom(BattleRole target)
        {
            Context.RoundBeginAction -= CalculateRemove;
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"(持续{Remain}回合)");
        }

        protected override void CalculateRemove()
        {
            if (Buff.Target == Context.Role)
            {
                base.CalculateRemove();
            }
        }
    }
}