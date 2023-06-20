#region

using InfinityWorldChess.ItemDomain.FoodDomain;
using UnityEngine;

#endregion

namespace InfinityWorldChess.Ugf
{
	public static class BpTransformExtension
	{
		public static void AddFlavorInfo(this Transform transform, IHasFlavor flavors)
		{
			transform.AddParagraph(
				"辛: " + flavors.SpicyLevel +
				"\r\n甘: " + flavors.SweetLevel + "\r\n酸: " + flavors.SourLevel +
				"\r\n苦: " + flavors.BitterLevel + "\r\n咸: " + flavors.SaltyLevel
			);
		}

		public static void AddMouthFeelInfo(this Transform transform, IHasMouthfeel mouthfeel)
		{
			transform.AddParagraph(
				"软硬: " + mouthfeel.HardLevel +
				"\r\n实酥: " + mouthfeel.LimpLevel +
				"\r\n韧脆: " + mouthfeel.WeakLevel +
				"\r\n枯滑: " + mouthfeel.OilyLevel +
				"\r\n糯爽: " + mouthfeel.SlipLevel +
				"\r\n老嫩: " + mouthfeel.SoftLevel
			);
		}
	}
}