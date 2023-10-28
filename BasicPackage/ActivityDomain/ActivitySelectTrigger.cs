using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    /// <summary>
    /// always exist when you need to trigger
    /// next activity in a group.
    /// </summary>
    public class ActivitySelectTrigger : ITrigger
    {
        [field: S] private string GroupName { get; set; }
        [field: S] private string ActivityName { get; set; }
        [field: S] private bool CurrentSuccess { get; set; }

        public void Invoke()
        {
            PlayerGameContext context = U.Get<PlayerGameContext>();
            ActivityGroup group = context.Activity
                .Find(u => u.Name == GroupName);
            if (group is null)
            {
                Debug.LogError($"Cannot find activity group: {GroupName}");
                return;
            }

            IActivity activity = group.CurrentActivity;
            activity.State = CurrentSuccess ? ActivityState.Success : ActivityState.Failed;
            activity.FinishActivity(group);

            activity = group.Activities.Find(u => u.Name == ActivityName);
            if (activity is null)
            {
                Debug.LogError($"Cannot find activity({ActivityName}) in group({GroupName}).");
                return;
            }

            group.CurrentActivity = activity;
            activity.StartActivity(group);
            activity.State = ActivityState.Received;
        }
    }
}