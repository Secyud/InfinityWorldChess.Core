using InfinityWorldChess.ActivityDomain;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.ActivityBundle
{
    public class ProtectionFeeActivityGroup:IActivity
    {
        public string ShowDescription { get; }
        public string ShowName { get; }
        public IObjectAccessor<Sprite> ShowIcon { get; }
        public void SetContent(Transform transform)
        {
            throw new System.NotImplementedException();
        }

        public ActivityState State { get; set; }
        public void SetActivity(ActivityGroup group)
        {
            throw new System.NotImplementedException();
        }
    }
}