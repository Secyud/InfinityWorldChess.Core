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
        public int CurrentIndex { get; set; }
        public IActivity GetCurrentActivity() => Activities[CurrentIndex];

        public virtual void Save(IArchiveWriter writer)
        {
            writer.Write((byte)State);
            writer.Write(CurrentIndex);
            writer.Write(ShowName);
        }

        public virtual void Load(IArchiveReader reader)
        {
            State = (ActivityState)reader.ReadByte();
            CurrentIndex = reader.ReadInt32();
            ShowName = reader.ReadString();
            U.Tm.TryWriteObject(this, ShowName);

            if (State == ActivityState.Received)
            {
                Activities[CurrentIndex].StartActivity(this);
            }
        }

        /// <summary>
        /// default activity change method.
        /// maybe not useful for some special activity.
        /// </summary>
        /// <param name="success"></param>
        public virtual void SetCurrentActivityFinished(bool success)
        {
            IActivity activity = GetCurrentActivity();
            activity.FinishActivity(this);
            activity.State = success ? ActivityState.Success : ActivityState.Failed;
            if (success)
            {
                CurrentIndex++;
                activity = GetCurrentActivity();

                if (activity is not null)
                {
                    activity.StartActivity(this);
                    activity.State = ActivityState.Received;
                    return;
                }
            }

            State = ActivityState.Failed;
        }
    }
}