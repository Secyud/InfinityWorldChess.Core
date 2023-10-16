﻿#region

using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain;
using Secyud.Ugf.TableComponents.SorterComponents;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public class ItemSorterType : SorterToggleDescriptor<IItem>
	{
		public override int SortValue(IItem target)
		{
			return target switch
			{
				IEquipment => 1,
				Food => 2,
				Drag => 3,
				SkillBookBase => 4,
				Manufacturable => 5,
				_ => 0
			};
		}

		public override string ShowName => "物品类型";
	}
}