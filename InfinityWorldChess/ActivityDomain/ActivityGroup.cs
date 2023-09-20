using System.Collections.Generic;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityGroup : IShowable,IArchivable
    {
        [field: S] public string ShowName { get; set; }
        [field: S] public string ShowDescription { get; set; }
        [field: S] public IObjectAccessor<Sprite> ShowIcon { get; set; }

        [field: S] public List<IActivity> Activities { get; } = new();
        
        public ActivityState State { get; set; }

        public bool Collapsed { get; set; }
        public int CurrentIndex { get; set; }
        public virtual void Save(IArchiveWriter writer)
        {
            writer.Write((byte)State);
            writer.Write(CurrentIndex);
            writer.Write(ShowName);
        }

        public virtual void Load(IArchiveReader reader)
        {
            State = (ActivityState)reader.ReadByte();
            CurrentIndex= reader.ReadInt32();
            ShowName = reader.ReadString();
            U.Tm.TryWriteObject(this,ShowName);

            if (State == ActivityState.Received)
            {
                Activities[CurrentIndex].SetActivity(this);
            }
        }
    }
}