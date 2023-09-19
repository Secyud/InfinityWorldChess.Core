using System.Collections.Generic;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityGroup : IShowable
    {
        [field: S] public string ShowName { get; set; }
        [field: S] public string ShowDescription { get; set; }
        [field: S] public IObjectAccessor<Sprite> ShowIcon { get; set; }

        [field: S] public List<Activity> Activities { get; } = new();
        
        public ActivityState State { get; set; }

        public bool Collapsed { get; set; }
    }
}