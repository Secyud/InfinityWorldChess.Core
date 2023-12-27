using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    /// <summary>
    /// living means state.
    /// kiling means difficulty.
    /// nimble means current index.
    /// defend means collapsed.
    /// </summary>
    [Guid("D751C9F8-64BE-EC75-10B1-1F128BD04E4E")]
    public sealed class ActivityGroup : IInstallable, IShowable, IArchivable, IDataResource
    {
        [field: S(0)] public string ResourceId { get; set; }
        [field: S(0)] public string Name { get; set; }
        [field: S(1)] public string Description { get; set; }
        [field: S(4)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(5)] public List<IActivity> Activities { get; } = new();

        public ActivityState State { get; set; }

        public bool Collapsed { get; set; }

        public void Save(IArchiveWriter writer)
        {
            this.SaveResource(writer);
            for (int i = 0; i < Activities.Count; i++)
            {
                Activities[i].Save(writer);
            }
        }

        public void Load(IArchiveReader reader)
        {
            this.LoadResource(reader);
            for (int i = 0; i < Activities.Count; i++)
            {
                Activities[i].Load(reader); 
            }
        }

        public void SetActivityResult(string id, bool success)
        {
            IActivity activity = Activities.FirstOrDefault(u => u.ResourceId == id);
            if (activity is null) return;
            activity.State = success ? ActivityState.Success : ActivityState.Failed;
        }

        public void SetActivityActive(string id)
        {
            IActivity activity = Activities.FirstOrDefault(u => u.ResourceId == id);
            if (activity is null) return;
            activity.State = ActivityState.Received;
        }

        public void Install()
        {
            State = ActivityState.Received;
            foreach (IActivity activity in Activities)
            {
                if (activity.State == ActivityState.Received)
                {
                    activity.Install();
                }
            }
        }

        public void UnInstall()
        {
            foreach (var activity in Activities)
            {
                if (activity.State == ActivityState.Received)
                {
                    activity.UnInstall();
                }
            }
        }
    }
}