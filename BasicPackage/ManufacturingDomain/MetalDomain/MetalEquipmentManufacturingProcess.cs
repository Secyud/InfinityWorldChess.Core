#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class
		MetalEquipmentManufacturingProcess : ManufacturingProcessBase<MetalEquipmentManufacturingContext,Equipment>
	{
		public byte Length { get; protected set; }

		public byte StartPosition { get; set; }

		protected MetalEquipmentManufacturingProcess(
			string name, string description, IObjectAccessor<Sprite> icon)
			: base(name, description, icon)
		{
		}
	}
}