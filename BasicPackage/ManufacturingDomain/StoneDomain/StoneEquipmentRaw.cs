using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public class StoneEquipmentRaw : EquipmentManufacturable<
		StoneEquipmentRaw, StoneEquipmentManufacturingProcess, StoneEquipmentManufacturingBlueprint,
		StoneEquipmentManufacturingContext, StoneEquipmentManufacturingProcessTf,
		StoneEquipmentManufacturingProperty>
	{
		[R(257)]public byte Volume { get; set; }

		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddParagraph("锻造格:".Point() + Volume);
		}
	}
}