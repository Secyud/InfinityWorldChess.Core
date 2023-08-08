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
				"辛: " + flavors.FlavorLevel[0] +
				"\r\n甘: " + flavors.FlavorLevel[1] + 
				"\r\n酸: " + flavors.FlavorLevel[2] +
				"\r\n苦: " + flavors.FlavorLevel[3] + 
				"\r\n咸: " + flavors.FlavorLevel[4]
			);
		}

		public static void AddMouthFeelInfo(this Transform transform, IHasMouthfeel mouthfeel)
		{
			transform.AddParagraph(
				"软硬: " + mouthfeel.MouthFeelLevel[0] +
				"\r\n实酥: " + mouthfeel.MouthFeelLevel[1] +
				"\r\n韧脆: " + mouthfeel.MouthFeelLevel[2] +
				"\r\n枯滑: " + mouthfeel.MouthFeelLevel[3] +
				"\r\n糯爽: " + mouthfeel.MouthFeelLevel[4] +
				"\r\n老嫩: " + mouthfeel.MouthFeelLevel[5]
			);
		}
	}
}