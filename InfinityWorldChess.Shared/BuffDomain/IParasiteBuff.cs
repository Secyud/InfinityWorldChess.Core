namespace InfinityWorldChess.BuffDomain
{
	public interface IParasiteBuff<TTarget> : IBuff<TTarget>
		where TTarget : class, IParasitifer<TTarget>
	{
		TTarget Parasitifer { set; get; }
	}
}