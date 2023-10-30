#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public interface IItem : IShowable, IHasContent, IHasSaveIndex,IHasScore
	{
	}

	public interface IHasScore
	{
		int Score { get; }
	}
}