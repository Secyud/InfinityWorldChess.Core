using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.Resource;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.Items
{
    public class ItemTemplate : ResourcedBase, IItem
    {
        [R(2)] public byte Score { get; set; }
        [R(3, true)] public string ShowDescription { get; set; }
        public string ShowName => Descriptor?.Name;
        public IObjectAccessor<Sprite> ShowIcon { get; set; }
        public int SaveIndex { get; set; }

        public virtual void SetContent(Transform transform)
        {
            transform.AddItemHeader(this);
        }

        protected override void SetDefaultValue()
        {
            ShowIcon = AtlasSpriteContainer.Create(IwcAb.Instance, Descriptor, 0);
        }
    }
}