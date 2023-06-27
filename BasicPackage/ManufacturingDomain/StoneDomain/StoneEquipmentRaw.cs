﻿using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public class StoneEquipmentRaw : EquipmentManufacturable<
		StoneEquipmentRaw, StoneEquipmentManufacturingProcess, StoneEquipmentManufacturingBlueprint,
		StoneEquipmentManufacturingContext, StoneEquipmentManufacturingProcessTf,
		StoneEquipmentManufacturingProperty>
	{
		[field: S(ID=257)]public byte Volume { get; set; }

		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddParagraph("锻造格:".Point() + Volume);
		}
	}
}