﻿#region

using Secyud.Ugf.BasicComponents;
using InfinityWorldChess.BuffDomain;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain.EquipmentDomain
{
	public class PropertyRect : MonoBehaviour
	{
		[SerializeField] private SImage[] Cells;


		public void OnInitialize(IAttachProperty property)
		{
			if (property is null)
				return;

			
			Cells[0].color = IntToColor(property.Living);
			Cells[1].color = IntToColor(property.Kiling);
			Cells[2].color = IntToColor(property.Nimble);
			Cells[3].color = IntToColor(property.Defend);
		}

		private static readonly Color[] Colors =
		{
			 new(1, 1, 1), new(0, 1, 0),
			 new(0, 1, 1), new(0, 0, 1),
			 new(1, 0, 1), new(1, 0, 0),
			 new(1, 1, 0), new(0, 0, 0)
		};

		private static Color IntToColor(int i)
		{
			if (i <= 0) return Colors[0];
			
			float scaled = 7- 3584/(i+512f);
			Color color0 = Colors[(int)scaled];
			Color color1 = Colors[(int)scaled + 1];
			float fraction = scaled - (int)scaled;
			Color result = new()
			{
				r = (1 - fraction) * color0.r + fraction * color1.r,
				g = (1 - fraction) * color0.g + fraction * color1.g,
				b = (1 - fraction) * color0.b + fraction * color1.b,
				a = 1
			};
			return result;
		}
	}
}