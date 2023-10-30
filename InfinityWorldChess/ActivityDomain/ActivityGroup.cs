using System.Collections.Generic;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public sealed class ActivityGroup : IShowable, IArchivable,IDataResource
    {
        [field: S] public string ResourceId { get; set; }
        [field: S] public string Name { get; set; }
        [field: S] public string Description { get; set; }
        [field: S] public IObjectAccessor<Sprite> Icon { get; set; }

        [field: S] public List<IActivity> Activities { get; } = new();

        public ActivityState State { get; set; }
        public bool Collapsed { get; set; }
        public IActivity CurrentActivity { get; set; }
        public void Save(IArchiveWriter writer)
        {
            writer.Write((byte)State);
            writer.Write(Name);
            for (int i = 0; i < Activities.Count; i++)
            {
                writer.Write((byte)Activities[i].State);
            }
        }

        public void Load(IArchiveReader reader)
        {
            State = (ActivityState)reader.ReadByte();
            this.LoadResource(reader);
            for (int i = 0; i < Activities.Count; i++)
            {
                ActivityState state = (ActivityState)reader.ReadByte();
                Activities[i].State = state;
                if (state == ActivityState.Received)
                {
                    CurrentActivity = Activities[i];
                }
            }
        }
    }
}