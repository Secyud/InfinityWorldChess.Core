using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class CountRecorder : EffectRecorder
    {
        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"(剩余{Remain}次)");
        }

        public override void Install(BattleRole target)
        {
            base.Install(target);
            if (BelongBuff.BuffEffect is TriggerEffect trigger)
            {
                trigger.ExtraAction += CalculateRemove;
            }
        }

        public override void UnInstall(BattleRole target)
        {
            base.UnInstall(target);
            if (BelongBuff.BuffEffect is TriggerEffect trigger)
            {
                trigger.ExtraAction -= CalculateRemove;
            }
        }
    }
}