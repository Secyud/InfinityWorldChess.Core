using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetLoading;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityResult : ResourcedBase, ICanBeShown, IHasContent
    {
        public string ShowName => Descriptor?.Name;
        [R(2,true)] public string ShowDescription { get; set; }
        public IObjectAccessor<Sprite> ShowIcon { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        protected override void SetDefaultValue()
        {
            ShowIcon = AtlasSpriteContainer.Create(
                IwcAb.Instance, Descriptor,0);
        }
    }
}