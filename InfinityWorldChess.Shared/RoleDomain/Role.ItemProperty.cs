#region

using InfinityWorldChess.ItemDomain;
using Secyud.Ugf.Archiving;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		public ItemProperty Item { get; } = new();

		public class ItemProperty : IList<IItem>, IArchivable
		{
			private readonly List<IItem> _items = new();

			public void Save(BinaryWriter writer)
			{
				writer.Write(Count);
				for (int i = 0; i < Count; i++)
				{
					IItem item = this[i];
					writer.WriteArchiving(item);
					item.SaveIndex = i;
				}
			}

			public void Load(BinaryReader reader)
			{
				Clear();
				int count = reader.ReadInt32();

				for (int i = 0; i < count; i++)
				{
					IItem item = reader.ReadArchiving<IItem>();
					item!.SaveIndex = i;
					this.AddLast(item);
				}
			}

			public IEnumerator<IItem> GetEnumerator() => _items.GetEnumerator();

			IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();

			public void Add(IItem item)
			{
				if (item is IOverloadedItem overloadedItem)
				{
					if (_items.FirstOrDefault(u => u.GetType() == overloadedItem.GetType()) 
						is IOverloadedItem exist)
					{
						if (exist == item)
							exist.Quantity += 1;
						else
							exist.Quantity+= overloadedItem.Quantity;
						return;
					}
				}
				_items.Add(item);
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

			public bool Remove(IItem item)
			{
				if (item is not IOverloadedItem overloadedItem ||
					overloadedItem.Quantity == 1) 
					return _items.Remove(item);

				overloadedItem.Quantity -= 1;
				return true;
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