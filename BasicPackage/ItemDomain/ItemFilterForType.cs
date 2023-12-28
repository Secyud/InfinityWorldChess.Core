#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using System;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingDomain.Equipments;
using Secyud.Ugf.TableComponents.FilterComponents;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public class ItemFilterForType<TItemType> : FilterToggleDescriptor<IItem>
	{
		public override bool Filter(IItem target)
		{
			return target is TItemType;
		}
	}
}