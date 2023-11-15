using System.Collections.Generic;
using InfinityWorldChess.ActivityDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityTemplates
{
    public class Activity: ActivityBase
    {
        [field: S(3)] public List<IActivityTrigger> Triggers { get; } = new();
        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            foreach (IActivityTrigger trigger in Triggers)
            {
                if (trigger is IHasContent content)
                {
                    content.SetContent(transform);
                }
            }
        }

        public override void StartActivity(ActivityGroup group)
        {
            foreach (IActivityTrigger trigger in Triggers)
            {
                trigger.StartActivity(group,this);
            }
        }

        public override void FinishActivity(ActivityGroup group)
        {
            foreach (IActivityTrigger trigger in Triggers)
            {
                trigger.FinishActivity(group,this);
            }
        }
    }
}