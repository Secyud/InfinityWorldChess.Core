using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.Items
{
    public class ItemTemplate :  IItem
    {
        [field: S] public string ShowName { get; set; }
        [field: S] public string ShowDescription { get; set; }

        [field: S] public IObjectAccessor<Sprite> ShowIcon { get; set; }

        [field: S] public byte Score { get; set; }

        public int SaveIndex { get; set; }

        public virtual void SetContent(Transform transform)
        {
            transform.AddItemHeader(this);
        }
    }
}