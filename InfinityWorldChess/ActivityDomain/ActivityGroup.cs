using System.Collections.Generic;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityGroup : IShowable, IArchivable
    {
        [field: S] public string ShowName { get; set; }
        [field: S] public string ShowDescription { get; set; }
        [field: S] public IObjectAccessor<Sprite> ShowIcon { get; set; }

        [field: S] public List<IActivity> Activities { get; } = new();

        public ActivityState State { get; set; }
        public bool Collapsed { get; set; }
        public IActivity CurrentActivity { get; set; }
        public virtual void Save(IArchiveWriter writer)
        {
            writer.Write((byte)State);
            writer.Write(ShowName);
            writer.Write(Activities.IndexOf(CurrentActivity));
        }

        public virtual void Load(IArchiveReader reader)
        {
            State = (ActivityState)reader.ReadByte();
            ShowName = reader.ReadString();
            U.Tm.TryWriteObject(this, ShowName);
            int index = reader.ReadInt32();
            if (index >= 0 && index < Activities.Count)
            {
                CurrentActivity = Activities[index];
            }
            if (State == ActivityState.Received)
            {
                CurrentActivity.StartActivity(this);
            }
        }
    }
}