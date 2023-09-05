using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.MetalDomain
{
	public class MetalEquipmentRaw : EquipmentManufacturable
	{
		[field: S ]public short MeltingPoint { get; set; }

		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddParagraph("熔点:".Point() + MeltingPoint);
		}
	}
}