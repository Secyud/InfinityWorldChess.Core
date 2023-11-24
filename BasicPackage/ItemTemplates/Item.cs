using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ItemTemplates
{
    public class Item :  IItem
    {
        [field: S] public string Name { get; set; }
        [field: S] public string Description { get; set; }
        [field: S] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S] public int Score { get; set; }
        [field: S] public string ResourceId { get; set; }
        public int SaveIndex { get; set; }

        public virtual void SetContent(Transform transform)
        {
            transform.AddItemHeader(this);
        }
    }
}