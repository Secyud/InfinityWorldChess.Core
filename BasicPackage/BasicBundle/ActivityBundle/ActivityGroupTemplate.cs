﻿using System.Collections.Generic;
using InfinityWorldChess.ActivityDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.ActivityBundle
{
    public class ActivityGroupTemplate:DataObject,IActivityGroup
    {
        public IActivity Active { get; set; }
        public IList<IActivity> Activities { get; } = new List<IActivity>();
        public ActivityState State { get; set; }
        public void SetNext(IActivity active)
        {
            throw new System.NotImplementedException();
        }
    }
}