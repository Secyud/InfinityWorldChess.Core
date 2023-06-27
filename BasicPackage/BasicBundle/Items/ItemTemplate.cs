using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.Items
{
    public class ItemTemplate : DataObject, IItem
    {
        [field: S(ID = 0, DataType = DataType.Initialed)]
        public string ShowDescription { get; set; }

        [field: S(ID = 1, DataType = DataType.Initialed)]
        public IObjectAccessor<Sprite> ShowIcon { get; set; }

        [field: S(ID = 0)] public byte Score { get; set; }

        public string ShowName => ObjectName;
        public int SaveIndex { get; set; }

        public virtual void SetContent(Transform transform)
        {
            transform.AddItemHeader(this);
        }
    }
}