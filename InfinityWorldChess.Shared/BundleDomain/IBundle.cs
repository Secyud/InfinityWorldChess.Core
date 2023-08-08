#region

using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.BundleDomain
{
	// all event is enormous, separate them
	public interface IBundle : IShowable, IHasContent
	{
		void OnGameCreation();
		void OnGameLoading();
	}
}