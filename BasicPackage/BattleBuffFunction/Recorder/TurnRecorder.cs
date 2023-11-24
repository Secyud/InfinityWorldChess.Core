using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffFunction
{
    public class TurnRecorder : BuffRecorderBase
    {
        public override void Install(BattleRole target)
        {
            Context.RoundBeginAction += CalculateRemove;
        }

        public override void UnInstall(BattleRole target)
        {
            Context.RoundBeginAction -= CalculateRemove;
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"(持续{Remain}回合)");
        }

        protected override void CalculateRemove()
        {
            if (Origin.Target == Context.Role)
            {
                base.CalculateRemove();
            }
        }
    }
}