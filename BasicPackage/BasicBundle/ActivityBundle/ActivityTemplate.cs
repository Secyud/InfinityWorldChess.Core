using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.ActivityBundle
{
    public class ActivityTemplate:ResourcedBase,IActivity
    {
        public string ShowName => Descriptor.Name;
        [R(2,false)]public string ShowDescription { get; set; }
        public IObjectAccessor<Sprite> ShowIcon { get; set; }
        public IActivityGroup Group { get; set; }
        public ActivityState State { get; set; }
        public int SaveIndex { get; set; }
        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public void OnReceive()
        {
            
            
        }

        protected override void SetDefaultValue()
        {
            ShowIcon =AtlasSpriteContainer.Create(IwcAb.Instance, Descriptor,0);
        }

    }
}