using InfinityWorldChess.ManufacturingDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.WoodDomain
{
	public class WoodEquipmentRaw : EquipmentManufacturable
	{
		[field: S ] public byte Width { get; set; }

		[field: S ] public byte Height { get; set; }
		
		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddParagraph("宽度:".Point() + Width);
			transform.AddParagraph("高度:".Point() + Height);
		}
	}
}