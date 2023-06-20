#region

using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.BuffDomain
{
	public interface IBuffCanBeShown<TTarget> : IBuff<TTarget>, ICanBeShown, IHasContent
	{

		// only visible and buff which is can be shown 
		bool Visible { get; }
	}
}