#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public interface IItem : ICanBeShown, IHasContent, IHasSaveIndex
	{
		byte Score { get; }
	}
}