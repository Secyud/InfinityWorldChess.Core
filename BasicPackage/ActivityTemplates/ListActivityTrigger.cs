using System.Collections.Generic;
using InfinityWorldChess.ActivityDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityTemplates
{
    public class ListActivityTrigger : IActivityTrigger, IHasContent
    {
        [field: S] public List<IActivityTrigger> Triggers { get; } = new();

        public void StartActivity(ActivityGroup group, IActivity activity)
        {
            foreach (IActivityTrigger trigger in Triggers)
            {
                trigger.StartActivity(group, activity);
            }
        }

        public void FinishActivity(ActivityGroup group, IActivity activity)
        {
            foreach (IActivityTrigger trigger in Triggers)
            {
                trigger.FinishActivity(group, activity);
            }
        }

        public void SetContent(Transform transform)
        {
            foreach (IActivityTrigger trigger in Triggers)
            {
                if (trigger is IHasContent content)
                {
                    content.SetContent(transform);
                }
            }
        }
    }
}