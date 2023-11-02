#region

using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain;
using System;
using InfinityWorldChess.ItemTemplates;
using Secyud.Ugf.TableComponents.FilterComponents;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public class ItemFilterToggleType : FilterToggleDescriptor<IItem>
	{
		private readonly Type _type;

		protected ItemFilterToggleType(Type type)
		{
			_type = type;
			Name = "ItemFilterType_" + _type.Name;
		}

		public override bool Filter(IItem target)
		{
			return _type.IsInstanceOfType(target);
		}

		public static FilterTriggerDescriptor<IItem> GetGroup()
		{
			return new FilterTriggerDescriptor<IItem>
			{
				Name = "物品类型",
				Filters =
				{
					new ItemFilterToggleType<IEquipment>(),
					new ItemFilterToggleType<IEdible>(),
					new ItemFilterToggleType<SkillBook>(),
					new ItemFilterToggleType<Manufacturable>(),
				}
			};
		}
	}

	public class ItemFilterToggleType<TItemType> : ItemFilterToggleType
	{
		public override bool Filter(IItem target)
		{
			return target is TItemType;
		}

		public ItemFilterToggleType() : base(typeof(TItemType))
		{
		}
	}
}