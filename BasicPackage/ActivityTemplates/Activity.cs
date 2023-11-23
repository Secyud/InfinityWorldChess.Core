using InfinityWorldChess.ActivityDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityTemplates
{
    public class Activity : ActivityBase
    {
        [field: S(3)] public IActivityTrigger Trigger { get; set; } 

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            if (Trigger is IHasContent content)
            {
                content.SetContent(transform);
            }
        }

        public override void StartActivity(ActivityGroup group)
        {
            Trigger?.StartActivity(group, this);
        }

        public override void FinishActivity(ActivityGroup group)
        {
            Trigger?.FinishActivity(group, this);
        }
    }
}