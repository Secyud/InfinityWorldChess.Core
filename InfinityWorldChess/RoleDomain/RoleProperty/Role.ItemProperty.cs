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

        public class ItemProperty : IList<IItem>, IArchivable
        {
            private readonly List<IItem> _items = new();
            public int Award { get; set; }

            public void Save(IArchiveWriter writer)
            {
                writer.Write(_items.Count);
                for (int i = 0; i < Count; i++)
                {
                    IItem item = this[i];
                    writer.WriteObject(item);
                    item.SaveIndex = i;
                }

                writer.Write(Award);
            }

            public void Load(IArchiveReader reader)
            {
                Clear();
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    IItem item = reader.ReadObject<IItem>();
                    item!.SaveIndex = i;
                    this.AddLast(item);
                }

                Award = reader.ReadInt32();
            }

            public IEnumerator<IItem> GetEnumerator() => _items.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();

            public void Add(IItem item)
            {
                if (item is IOverloadedItem oItem)
                {
                    Type type = item.GetType();
                    string name = item.ShowName;
                    IOverloadedItem min = _items
                        .Where(u => u.GetType() == type && u.ShowName == name)
                        .Cast<IOverloadedItem>()
                        .OrderBy(u=>u.Quantity)
                        .FirstOrDefault();
                    
                    if (min is not null)
                    {
                        min.Quantity +=  oItem.Quantity;
                        if (min.Quantity > SharedConsts.MaxOverloadedCount)
                        {
                            oItem.Quantity = min.Quantity - SharedConsts.MaxOverloadedCount;
                            min.Quantity = SharedConsts.MaxOverloadedCount;
                            _items.AddLast(oItem);
                        }
                    }
                    else
                    {
                        _items.AddLast(oItem);
                    }
                }
                else
                {
                    _items.AddLast(item);
                }
            }

            public bool Remove(IItem item)
            {
                if (item is IOverloadedItem oItem)
                {
                    Type type = item.GetType();
                    string name = item.ShowName;
                    List<IOverloadedItem> items = _items
                        .Where(u => u.GetType() == type && u.ShowName == name)
                        .Cast<IOverloadedItem>()
                        .OrderByDescending(u=>u.Quantity)
                        .ToList();
                    int count = oItem.Quantity;

                    while (count!=0)
                    {
                        if (!items.Any())
                        {
                            return false;
                        }
                        IOverloadedItem min = items.Last();
                        items.RemoveAt(items.Count - 1);
                        min.Quantity -=  count;
                        if (min.Quantity <= 0)
                        {
                            count = - min.Quantity;
                            _items.Remove(min);
                        }
                        else
                        {
                            count = 0;
                        }
                    }
                }
                else
                {
                    return _items.Remove(item);
                }

                return true;
            }
            
            public void Clear()
            {
                _items.Clear();
            }

            public bool Contains(IItem item) => _items.Contains(item);

            public void CopyTo(IItem[] array, int arrayIndex)
            {
                _items.CopyTo(array, arrayIndex);
            }

            public int Count => _items.Count;

            public bool IsReadOnly => ((ICollection<IItem>)_items).IsReadOnly;

            public int IndexOf(IItem item) => _items.IndexOf(item);

            public void Insert(int index, IItem item)
            {
                _items.Insert(index, item);
            }

            public void RemoveAt(int index)
            {
                _items.RemoveAt(index);
            }

            public IItem this[int index]
            {
                get => _items[index];
                set => _items[index] = value;
            }
        }
    }
}