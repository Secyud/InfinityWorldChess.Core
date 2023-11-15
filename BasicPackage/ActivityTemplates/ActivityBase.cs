using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityTemplates
{
    public abstract class ActivityBase : IActivity
    {
        [field: S(0)] public string Name { get; set; }
        [field: S(0)] public string ResourceId { get; set; }
        [field: S(1)] public string Description { get; set; }
        [field: S(256)] public IObjectAccessor<Sprite> Icon { get; set; }
        public ActivityState State { get; set; }

        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public virtual void StartActivity(ActivityGroup group)
        {
        }

        public virtual void FinishActivity(ActivityGroup group)
        {
        }
    }
}