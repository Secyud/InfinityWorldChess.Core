#region

using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.BuffDomain
{
	public interface IBuffShowable<TTarget> : IBuff<TTarget>, IShowable, IHasContent
	{
		// only visible and buff which is can be shown 
		bool Visible { get; }
	}
}