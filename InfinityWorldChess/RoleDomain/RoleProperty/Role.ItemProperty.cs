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

            public void Save(IArchiveWriter writer)
            {
                writer.Write(_items.Count);
                for (int i = 0; i < Count; i++)
                {
                    IItem item = this[i];
                    writer.WriteObject(item);
                    item.SaveIndex = i;
                }
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
            }

            public IEnumerator<IItem> GetEnumerator() => _items.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();

            public void Add(IItem item, int count)
            {
                if (item is IOverloadedItem)
                {
                    Type itemType = item.GetType();
                    IItem temp = _items.FirstOrDefault(u => u.GetType() == itemType);

                    if (temp is IOverloadedItem oiTmp)
                    {
                        oiTmp.Quantity += count;
                    }
                    else
                    {
                        _items.AddFirst(item);
                    }
                }
                else
                {
                    _items.AddLast(item);
                }
            }

            public bool Remove(IItem item, int count)
            {
                if (item is IOverloadedItem)
                {
                    Type itemType = item.GetType();
                    int index = -1;
                    IItem temp = null;

                    for (int i = 0; i < _items.Count; i++)
                    {
                        if (_items[i].GetType() == itemType)
                        {
                            temp = _items[i];
                            index = i;
                            break;
                        }
                    }

                    if (temp is IOverloadedItem oiTmp)
                    {
                        oiTmp.Quantity -= count;
                        if (oiTmp.Quantity <= 0)
                            _items.RemoveAt(index);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return _items.Remove(item);
                }

                return true;
            }

            public void Add(IItem item)
            {
                Add(item, 1);
            }

            public bool Remove(IItem item)
            {
                return Remove(item, 1);
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