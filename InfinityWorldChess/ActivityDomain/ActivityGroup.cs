using System.Collections.Generic;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityGroup : IShowable, IArchivable
    {
        [field: S] public string Name { get; set; }
        [field: S] public string Description { get; set; }
        [field: S] public IObjectAccessor<Sprite> Icon { get; set; }

        [field: S] public List<IActivity> Activities { get; } = new();

        public ActivityState State { get; set; }
        public bool Collapsed { get; set; }
        public IActivity CurrentActivity { get; set; }
        public virtual void Save(IArchiveWriter writer)
        {
            writer.Write((byte)State);
            writer.Write(Name);
            writer.Write(Activities.IndexOf(CurrentActivity));
        }

        public virtual void Load(IArchiveReader reader)
        {
            State = (ActivityState)reader.ReadByte();
            Name = reader.ReadString();
            U.Tm.TryWriteObject(this, Name);
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