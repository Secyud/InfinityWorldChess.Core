using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public abstract class BuffInteractionTrigger : TriggerEffect, IHasContent, IBuffInteractionEffect
    {
        [field: S] public IBuffInteractionEffect Effect { get; set; }
        [field: S] public ILimitCondition Limit { get; set; }

        public int Priority => Effect?.Priority ?? 0;

        public void Active(SkillInteraction target)
        {
            if (Limit is not null &&
                !Limit.CheckLimit(target))
            {
                return;
            }
            Effect?.Active(target);
            ExtraActionInvoke();
        }

        public virtual void SetContent(Transform transform)
        {
            if (Limit is IHasContent limitContent)
            {
                limitContent.SetContent(transform);
            }
            
            if (Effect is IHasContent content)
            {
                content.SetContent(transform);
            }
        }
    }
}