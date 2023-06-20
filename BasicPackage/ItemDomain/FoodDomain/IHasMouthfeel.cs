namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public interface IHasMouthfeel
	{
		float HardLevel { get; set; } //软硬
		float LimpLevel { get; set; } //实酥
		float WeakLevel { get; set; } //韧脆
		float OilyLevel { get; set; } //枯滑
		float SlipLevel { get; set; } //糯爽
		float SoftLevel { get; set; } //老嫩
	}
}