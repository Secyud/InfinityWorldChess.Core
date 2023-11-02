using InfinityWorldChess.ActivityAccessors;
using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.NormalTriggers
{
    /// <summary>
    /// always exist when you need to trigger
    /// next activity in a group.
    /// </summary>
    public class SelectActivityInGroup : ITrigger
    {
        [field: S] private PlayerActivityGroup GroupAccessor { get; set; }
        [field: S] private string ActivityId { get; set; }
        [field: S] private bool CurrentSuccess { get; set; }

        public void Invoke()
        {
            ActivityGroup group = GroupAccessor.Value;
            if (group is null)return;

            IActivity activity = group.CurrentActivity;
            
            activity.State = CurrentSuccess ? ActivityState.Success : ActivityState.Failed;
            activity.FinishActivity(group);

            activity = group.Activities
                .Find(u => u.ResourceId == ActivityId);
            if (activity is null)
            {
#if DEBUG
                Debug.LogError($"Cannot find activity({ActivityId}) in group({group.Name}).");
#endif
                return;
            }

            group.CurrentActivity = activity;
            activity.StartActivity(group);
            activity.State = ActivityState.Received;
        }
    }
}