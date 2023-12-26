#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public interface IItem : IShowable, IHasContent, IHasSaveIndex,IHasScore,IDataResource
	{
	}

	public interface IHasScore
	{
		int Score { get; }
	}
}