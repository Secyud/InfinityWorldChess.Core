using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Collections;

namespace InfinityWorldChess.InteractionDomain
{
	public interface IInteractionGlobalService
	{
		RegistrableDictionary<int, IInteractionUnitHead> TotalInteractions { get; }
		RegistrableList<IFreeInteractionUnit> FreeInteractions { get; }
		IInteractionUnit GenerateFreeInteraction(Role left, Role right);
	}
}