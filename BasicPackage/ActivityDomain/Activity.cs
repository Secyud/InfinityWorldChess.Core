using System.Collections.Generic;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class Activity: IActivity
    {
        [field: S] public string ShowDescription { get; set; }
        [field: S] public string ShowName { get; set; }
        [field: S] public IObjectAccessor<Sprite> ShowIcon { get; set; }
        [field: S] public List<IActivityTrigger> Triggers { get; } = new();
        public ActivityState State { get; set; }
        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
            foreach (IActivityTrigger trigger in Triggers)
            {
                if (trigger is IHasContent content)
                {
                    content.SetContent(transform);
                }
            }
        }

        public void StartActivity(ActivityGroup group)
        {
            foreach (IActivityTrigger trigger in Triggers)
            {
                trigger.StartActivity(group,this);
            }
        }

        public void FinishActivity(ActivityGroup group)
        {
            foreach (IActivityTrigger trigger in Triggers)
            {
                trigger.FinishActivity(group,this);
            }
        }
    }
}