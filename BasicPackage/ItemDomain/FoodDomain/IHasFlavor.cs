namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public interface IHasFlavor
	{
		// float SpicyLevel { get; set; }
		// float SweetLevel { get; set; }
		// float SourLevel  { get; set; }
		// float BitterLevel { get; set; }
		// float SaltyLevel { get; set; }
		
		float[] FlavorLevel { get; }
	}
}