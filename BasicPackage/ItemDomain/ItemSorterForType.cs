#region

using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.ManufacturingDomain.Equipments;
using Secyud.Ugf.TableComponents.SorterComponents;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public class ItemSorterForType : SorterToggleDescriptor<IItem>
	{
		public override int SortValue(IItem target)
		{
			return target switch
			{
				Equipment => 1,
				Consumable => 2,
				ReadableItem => 4,
				EquipmentMaterial => 5,
				_ => 0
			};
		}

		public override string Name => "物品类型";
	}
}