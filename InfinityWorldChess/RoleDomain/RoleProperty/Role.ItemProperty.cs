#region

using System;
using InfinityWorldChess.ItemDomain;
using Secyud.Ugf.Archiving;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        public ItemProperty Item { get; } = new();

        public class ItemProperty : IArchivable
        {
            private readonly List<IItem> _items = new();
            private readonly SortedDictionary<string,IOverloadedItem> _oItems = new();
            public int Award { get; set; }

            public void Save(IArchiveWriter writer)
            {
                writer.Write(_items.Count);
                for (int i = 0; i < _items.Count; i++)
                {
                    IItem item = _items[i];
                    writer.WriteObject(item);
                    item.SaveIndex = i;
                }

                List<IOverloadedItem> oItemList = _oItems.Values.ToList();
                writer.Write(oItemList.Count);
                for (int i = 0; i < oItemList.Count; i++)
                {
                    IOverloadedItem item = oItemList[i];
                    writer.WriteObject(item);
                    item.SaveIndex = i;
                }

                writer.Write(Award);
            }

            public void Load(IArchiveReader reader)
            {
                _items.Clear();
                _oItems.Clear();
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    IItem item = reader.ReadObject<IItem>();
                    item!.SaveIndex = i;
                    Add(item);
                }
                count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    IOverloadedItem item = reader.ReadObject<IOverloadedItem>();
                    item!.SaveIndex = i;
                    Add(item);
                }

                Award = reader.ReadInt32();
            }
            
            public void Add(IItem item)
            {
                if (item is IOverloadedItem oItem)
                {
                    _oItems.TryGetValue(item.ResourceId, out IOverloadedItem existItem);

                    if (existItem is not null)
                    {
                        existItem.Quantity += oItem.Quantity;
                    }
                    else
                    {
                        _oItems[item.ResourceId] = oItem;
                    }
                }
                else
                {
                    _items.AddLast(item);
                }
            }

            public bool Remove(IItem item,int count)
            {
                if (item is IOverloadedItem)
                {
                    _oItems.TryGetValue(item.ResourceId, out IOverloadedItem existItem);

                    if (existItem is not null)
                    {
                        existItem.Quantity -= count;

                        if (existItem.Quantity <= 0)
                        {
                            _oItems.Remove(item.ResourceId);
                        }
                    }
                }
                else
                {
                    return _items.Remove(item);
                }

                return true;
            }

            public IItem this[int i] => i < _items.Count ? _items[i] : null;
            
            public IList<IItem> All()
            {
                List<IItem> ret = _oItems.Values.Cast<IItem>().ToList();
                ret.AddRange(_items);
                return ret;
            }
        }
    }
}