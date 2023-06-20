using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.InteractionDomain
{
	public interface IFreeInteractionUnit:IInteractionUnitHead
	{
		string Title { get; }
		bool VisibleFor(Role left, Role right);
	}
}