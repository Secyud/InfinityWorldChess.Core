using System.Runtime.InteropServices;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    [Guid("341A7C54-0337-B875-C54F-7958E89B850C")]
    public class Activity : IActivity
    {
        [field: S(0)] public string Name { get; set; }
        [field: S(0)] public string ResourceId { get; set; }
        [field: S(1)] public string Description { get; set; }
        [field: S(2)] private ActivityState InnerState { get; set; }
        [field: S(32)] public IObjectAccessor<Sprite> Icon { get; set; }

        /// <summary>
        /// install provide the finish trigger,
        /// uninstall remove the trigger.
        /// </summary>
        [field: S(16)]
        public IInstallable Installable { get; set; }

        public ActivityState State
        {
            get => InnerState;
            set
            {
                if (Equals(InnerState, value))
                {
                    return;
                }

                if (InnerState is ActivityState.Received)
                {
                    UnInstall();
                    switch (value)
                    {
                        case ActivityState.NotReceived:
                            MessageScope.Instance.AddMessage($"重置任务: {Name}");
                            break;
                        case ActivityState.Received: break;
                        case ActivityState.Failed: 
                            MessageScope.Instance.AddMessage($"任务失败: {Name}");
                            break;
                        case ActivityState.Success:  
                            MessageScope.Instance.AddMessage($"任务完成: {Name}");
                            break;
                        default:                     break;
                    }
                }
                else if (value is ActivityState.Received)
                {
                    Install();
                    MessageScope.Instance.AddMessage($"获取任务：{Name}");
                }

                InnerState = value;
            }
        }

        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
            Installable.TrySetContent(transform);
        }

        public void Install()
        {
            Installable?.Install();
        }

        public void UnInstall()
        {
            Installable?.UnInstall();
        }

        public void Save(IArchiveWriter writer)
        {
            writer.Write((byte)InnerState);
        }

        public void Load(IArchiveReader reader)
        {
            InnerState = (ActivityState)reader.ReadByte();
        }
    }
}