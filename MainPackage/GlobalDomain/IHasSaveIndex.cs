namespace InfinityWorldChess.GlobalDomain
{
	/// <summary>
	/// 保存索引，保存列表时的临时索引，在保存引用类型时，如果存在引用，只需保存相应的索引。
	/// 因为并未设置全局Id这种东西，所以用索引代替。
	/// </summary>
	public interface IHasSaveIndex
	{
		int SaveIndex { get; set; }
	}
}