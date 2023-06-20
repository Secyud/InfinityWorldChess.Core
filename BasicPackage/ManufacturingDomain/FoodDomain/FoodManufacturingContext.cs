using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using System.Linq;
using InfinityWorldChess.GlobalDomain;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public class FoodManufacturingContext : FlavorManufacturingContextBase
	<Food, FoodManufacturingComponent, FoodManufacturingProcess, FoodManufacturingContext,
		FoodManufacturingProcessTf, FoodManufacturingProperty>, ISingleton
	{

		public FoodManufacturingData FoodData { get; private set; }

		public class FoodManufacturingData : IHasFlavor, IHasMouthfeel
		{
			public readonly Role.ItemProperty RoleItem;

			public FoodRaw MainRaw { get; set; }

			public FoodRaw SupportRaw { get; set; }

			public FoodManufacturingData(Role mainRole, FoodManufacturingContext context)
			{
				RoleItem = mainRole.Item;
				FoodManufacturingComponent factory = context.Factory.Value;
				factory.MainRaw.Icon.gameObject
					.GetOrAddButton(OnSelectMainRawButtonClick);
				factory.SupportRaw.Icon.gameObject
					.GetOrAddButton(OnSelectSupportRawButtonClick);
			}


			public void OnSelectMainRawButtonClick()
			{
				Og.ScopeFactory.GetScope<GlobalScope>().OnItemSelectionOpen(
					RoleItem.Where(u => u is FoodRaw).ToList(),
					i => MainRaw = i as FoodRaw
				);
			}

			public void OnSelectSupportRawButtonClick()
			{
				Og.ScopeFactory.GetScope<GlobalScope>().OnItemSelectionOpen(
					RoleItem.Where(u => u is FoodRaw).ToList(),
					i => SupportRaw = i as FoodRaw
				);
			}

			public float SpicyLevel { get; set; }

			public float SweetLevel { get; set; }

			public float SourLevel { get; set; }

			public float BitterLevel { get; set; }

			public float SaltyLevel { get; set; }

			public float HardLevel { get; set; }

			public float LimpLevel { get; set; }

			public float WeakLevel { get; set; }

			public float OilyLevel { get; set; }

			public float SlipLevel { get; set; }

			public float SoftLevel { get; set; }
		}

		protected override void RunProcess(Food drag)
		{
			foreach (FoodManufacturingProcess process in Data.Processes)
			{
				process.Flavor?.CopyFlavorTo(drag, 0.1f);
				process.Process(this,drag);
				FoodData.MainRaw.ProcessFood(this);
			}
		}

		protected override Food InitFlavor()
		{
			if (FoodData.MainRaw is null)
				return null;

			Food food = new Food();
			FoodData.MainRaw.InitFood(this,food);
			
			FoodData.MainRaw.CopyFlavorTo(food);
			FoodData.MainRaw.CopyMouthfeelTo(food);

			if (FoodData.SupportRaw is not null)
			{
				FoodData.SupportRaw.CopyFlavorTo(food,0.7f);
				FoodData.SupportRaw.CopyMouthfeelTo(food,0.7f);
			}
			
			return food;
		}

		public override bool OnCreation(Role mainRole, Role manufacturingRole = null)
		{
			if (base.OnCreation(mainRole, manufacturingRole))
			{
				FoodData = new FoodManufacturingData(mainRole, this);
				return true;
			}
			return false;
		}


		public FoodManufacturingContext(IwcAb ab) : base(ab)
		{
		}
	}
}