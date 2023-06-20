namespace InfinityWorldChess.BuffDomain
{
	public interface IBuffFactory<TTarget>
	{
		// Some buff need to parasitise some object like weapon, wearable, item.
		// But some buff can also exist dependence.
		// Maybe this interface is no need.
		IBuff<TTarget> Get();
	}
}