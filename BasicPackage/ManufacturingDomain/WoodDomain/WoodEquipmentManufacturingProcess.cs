#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using Secyud.Ugf;
using System;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public abstract class
		WoodEquipmentManufacturingProcess : ManufacturingProcessBase
			<WoodEquipmentManufacturingContext,Equipment>
	{
		public byte PositionX { get; set; }

		public byte PositionY { get; set; }

		public PairStruct<byte, byte>[] Range { get; protected set; }

		protected WoodEquipmentManufacturingProcess(
			string name, string description, IObjectAccessor<Sprite> icon) : base(
			name,
			description, icon
		)
		{
		}
	}
}