using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.InteractionDomain
{
	public interface IInteractionGlobalService:IRegistry
	{
		RegistrableDictionary<int, IInteractionUnitHead> TotalInteractions { get; }
		RegistrableList<IFreeInteractionUnit> FreeInteractions { get; }
		IInteractionUnit GenerateFreeInteraction(Role left, Role right);
	}
}