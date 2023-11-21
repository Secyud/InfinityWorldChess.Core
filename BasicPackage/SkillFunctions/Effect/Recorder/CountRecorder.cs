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

        public override void SetProperty(IBuffProperty skill)
        {
            if (Buff.BuffEffect is TriggerEffect trigger)
            {
                trigger.ExtraAction += CalculateRemove;
            }
        }
    }
}