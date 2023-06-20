using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;
using System;
using System.Linq;

namespace InfinityWorldChess.InteractionDomain
{
	public class IwcInteractionGlobalService:IInteractionGlobalService,ISingleton
	{
		public RegistrableDictionary<int, IInteractionUnitHead> TotalInteractions { get; } = new();

		public RegistrableList<IFreeInteractionUnit> FreeInteractions { get; } = new();

		public IInteractionUnit GenerateFreeInteraction(Role left, Role right)
		{
			InteractionScope.Context.LeftRole = left;
			InteractionScope.Context.RightRole = right;
			FreeInteractionBegin freeInteractionBegin = new();
			foreach (IFreeInteractionUnit interaction in 
				FreeInteractions.Get().Where(interaction => interaction.VisibleFor(left,right)))
				freeInteractionBegin.Interactions.Add(
					new Tuple<string, IInteractionUnit>(interaction.Title,interaction));
			
			return freeInteractionBegin;
		}
	}
}