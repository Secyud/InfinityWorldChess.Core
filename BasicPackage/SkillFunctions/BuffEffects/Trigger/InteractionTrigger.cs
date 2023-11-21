using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public abstract class InteractionTrigger : TriggerEffect, IHasContent, IInteractionEffect
    {
        [field: S] public IInteractionEffect Effect { get; set; }
        [field: S] public ITriggerLimit Limit { get; set; }

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
            if (Effect is IHasContent content)
            {
                content.SetContent(transform);
            }
        }
    }
}