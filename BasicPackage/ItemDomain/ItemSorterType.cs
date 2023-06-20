#region

using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public class ItemSorterType : ISorterRegistration<IItem>
	{
		public int SortValue(IItem target)
		{
			return target switch
			{
				IEquipment => 1,
				IEdible => 2,
				SkillBookBase => 3,
				ManufacturingBlueprintBase => 4,
				Manufacturable => 5,
				_ => 0
			};
		}

		public string ShowName => "物品类型";

		public string ShowDescription => null;

		public IObjectAccessor<Sprite> ShowIcon => null;

		public bool? Enabled { get; set; }
	}
}