#region

using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain;
using Secyud.Ugf.TableComponents;
using System;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public class ItemFilterType : FilterRegistration<IItem>
	{
		private readonly Type _type;

		protected ItemFilterType(Type type)
		{
			_type = type;
		}

		public override string ShowName => "ItemFilterType_" + _type.Name;

		public override bool Filter(IItem target)
		{
			return _type.IsInstanceOfType(target);
		}

		public static FilterRegistrationGroup<IItem> GetGroup()
		{
			return new FilterRegistrationGroup<IItem>
			{
				ShowName = "物品类型",
				Filters =
				{
					new ItemFilterType<IEquipment>(),
					new ItemFilterType<IEdible>(),
					new ItemFilterType<SkillBookBase>(),
					new ItemFilterType<ManufacturingBlueprintBase>(),
					new ItemFilterType<Manufacturable>(),
				}
			};
		}
	}

	public class ItemFilterType<TItemType> : ItemFilterType
	{
		public override bool Filter(IItem target)
		{
			return target is TItemType;
		}

		public ItemFilterType() : base(typeof(TItemType))
		{
		}
	}
}