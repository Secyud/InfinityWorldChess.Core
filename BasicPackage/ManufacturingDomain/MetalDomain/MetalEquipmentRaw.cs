using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public class MetalEquipmentRaw :
		EquipmentManufacturable<
			MetalEquipmentRaw, MetalEquipmentManufacturingProcess, MetalEquipmentManufacturingBlueprint,
			MetalEquipmentManufacturingContext, MetalEquipmentManufacturingProcessTf,
			MetalEquipmentManufacturingProperty>,
		IHasMeltingPoint
	{
		[field: S(ID=257)]public short MeltingPoint { get; set; }

		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddParagraph("熔点:".Point() + MeltingPoint);
		}
	}
}