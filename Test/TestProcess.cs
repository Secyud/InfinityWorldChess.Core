#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ManufacturingDomain;
using UnityEngine;

#endregion

namespace InfinityWorldChess
{
	public class TestProcess : StoneEquipmentManufacturingProcess
	{

		public TestProcess() : base("name", "description", null)
		{
		}

		public override void Process(StoneEquipmentManufacturingContext contextBase,Equipment equipment)
		{
		}

		public override Color Color => Color.blue;


		public override byte RangeStart { get; } = 0;

		public override byte RangeEnd { get; } = 1;
	}
}