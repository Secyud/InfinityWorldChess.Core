#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class StoneEquipmentManufacturingProcess :
		ManufacturingProcessBase<StoneEquipmentManufacturingContext,Equipment>
	{
		public short Position { get; set; }

		public abstract byte RangeStart { get; }

		public abstract byte RangeEnd{ get; }

		protected StoneEquipmentManufacturingProcess(
			string name, string description, IObjectAccessor<Sprite> icon) :
			base(name, description, icon)
		{
		}

		public override void SetContent(Transform transform)
		{
			base.SetContent(transform);
			transform.AddParagraph($"{Og.L["目标槽位"]}: {RangeStart}-{RangeEnd}");
		}

	}
}