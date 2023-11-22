using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public abstract class BuffActionTrigger : TriggerEffect, IHasContent,IBuffActionEffect
    {
        [field: S] public IBuffActionEffect Effect { get; set; }
        [field: S] public ILimitCondition Limit { get; set; }

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