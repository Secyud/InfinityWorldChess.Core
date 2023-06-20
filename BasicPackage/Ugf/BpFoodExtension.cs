using InfinityWorldChess.ItemDomain.FoodDomain;
using System;

namespace InfinityWorldChess.Ugf
{
	public static class BpFoodExtension
	{


		public static void CopyFlavorTo(this IHasFlavor lft, IHasFlavor rht, float a = 1)
		{
			rht.SpicyLevel += lft.SpicyLevel * a;
			rht.SweetLevel += lft.SweetLevel * a;
			rht.SourLevel += lft.SourLevel * a;
			rht.BitterLevel += lft.BitterLevel * a;
			rht.SaltyLevel += lft.SaltyLevel * a;
		}

		public static float GetFlavor(this IHasFlavor lft, int a)
		{
			a %= 5;
			if (a < 0)
				a += 5;

			return a switch
			{
				0 => lft.SpicyLevel,
				1 => lft.SaltyLevel,
				2 => lft.SourLevel,
				3 => lft.BitterLevel,
				4 => lft.SweetLevel,
				_ => throw new ArgumentOutOfRangeException(nameof(a), a, null)
			};
		}

		public static void SetFlavor(this IHasFlavor lft, int a, float value)
		{
			a %= 5;
			if (a < 0)
				a += 5;
			switch (a)
			{
			case 0:
				lft.SpicyLevel += value;
				return;
			case 1:
				lft.SaltyLevel += value;
				return;
			case 2:
				lft.SourLevel += value;
				return;
			case 3:
				lft.BitterLevel += value;
				return;
			case 4:
				lft.SweetLevel += value;
				return;
			}
		}

		public static void CopyMouthfeelTo(this IHasMouthfeel lft, IHasMouthfeel rht, float a = 1)
		{
			rht.HardLevel += lft.HardLevel * a;
			rht.LimpLevel += lft.LimpLevel * a;
			rht.WeakLevel += lft.WeakLevel * a;
			rht.OilyLevel += lft.OilyLevel * a;
			rht.SlipLevel += lft.SlipLevel * a;
			rht.SoftLevel += lft.SoftLevel * a;
		}
	}
}