#region

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

			
			Cells[0].color = ByteToColor(property.Living);
			Cells[1].color = ByteToColor(property.Kiling);
			Cells[2].color = ByteToColor(property.Nimble);
			Cells[3].color = ByteToColor(property.Defend);
		}

		private const float LOG = 2.6859453246115805f;

		private static readonly Color[] Colors =
		{
			 new(1, 1, 1), new(0, 1, 0),
			 new(0, 1, 1), new(0, 0, 1),
			 new(1, 0, 1), new(1, 0, 0),
			 new(1, 1, 0), new(0, 0, 0)
		};

		private static Color ByteToColor(byte i)
		{
			float scaled = i / 37.0f;
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