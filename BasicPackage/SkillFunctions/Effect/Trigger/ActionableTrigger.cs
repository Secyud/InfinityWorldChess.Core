using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public abstract class ActionableTrigger : TriggerEffect, IHasContent,IActionableEffect
    {
        [field: S] public IActionableEffect Effect { get; set; }
        [field: S] public ITriggerLimit Limit { get; set; }

        public void Active()
        {
            if (Limit is not null &&
                !Limit.CheckLimit(null))
            {
                return;
            }
            Effect?.Active();
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