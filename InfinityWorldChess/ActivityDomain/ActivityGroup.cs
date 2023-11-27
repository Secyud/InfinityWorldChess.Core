using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.Ugf;
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
    public sealed class ActivityGroup :IInstallable, IShowable, IArchivable, IDataResource, IAttachProperty
    {
        [field: S(0)] public string ResourceId { get; set; }
        [field: S(0)] public string Name { get; set; }
        [field: S(1)] public string Description { get; set; }
        [field: S(3)] public byte Living { get; set; }
        [field: S(3)] public byte Kiling { get; set; }
        [field: S(3)] public byte Nimble { get; set; }
        [field: S(3)] public byte Defend { get; set; }
        [field: S(4)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(5)] public List<IActivity> Activities { get; } = new();

        public ActivityState State
        {
            get => (ActivityState)Living;
            set => Living = (byte)value;
        }

        public byte Current
        {
            get => Nimble;
            set => Nimble = value;
        }

        public bool Collapsed
        {
            get => Defend > 0;
            set => Defend = (byte)(value ? 1 : 0);
        }

        public void Save(IArchiveWriter writer)
        {
            this.SaveResource(writer);
            this.SaveProperty(writer);
            for (int i = 0; i < Activities.Count; i++)
            {
                Activities[i].SaveProperty(writer);
            }
        }

        public void Load(IArchiveReader reader)
        {
            this.LoadResource(reader);
            this.LoadProperty(reader);
            for (int i = 0; i < Activities.Count; i++)
            {
                Activities[i].LoadProperty(reader);
            }
        }

        public void SetNextActivity(string id, bool success)
        {
            if (State == ActivityState.Received &&
                Current < Activities.Count)
            {
                IActivity activity = Activities[Current];
                activity.UnInstallFrom();
                activity.Nimble = (byte)(success ? ActivityState.Success : ActivityState.Failed);
                MessageScope.Instance.AddMessage($"任务{(success ? "完成" : "失败")}：{activity.Name}");
            }

            for (int i = 0; i < Activities.Count; i++)
            {
                IActivity activity = Activities[i];
                if (activity.ResourceId == id)
                {
                    Current = (byte)i;
                    activity.Living = (byte)ActivityState.Received;
                    activity.InstallFrom();
                    MessageScope.Instance.AddMessage($"获取任务：{activity.Name}");
                }

                activity.Nimble = (byte)i;
            }
        }

        public void InstallFrom()
        {
            if (State == ActivityState.Received && 
                Current < Activities.Count)
            {
                Activities[Current].InstallFrom();
                Activities[Current].Living = (byte)ActivityState.Received;
            }
        }

        public void UnInstallFrom()
        {
            if (State == ActivityState.Received && 
             Current < Activities.Count)
            {
                Activities[Current].UnInstallFrom();
                Activities[Current].Living = (byte)ActivityState.NotReceived;
            }
        }
    }
}