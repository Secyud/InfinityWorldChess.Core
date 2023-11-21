using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class TurnRecorder : EffectRecorder
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
            if (Buff.Role == Context.Role)
            {
                base.CalculateRemove();
            }
        }
    }
}