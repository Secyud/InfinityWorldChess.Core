using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Collections;
using System;
using System.Linq;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.InteractionDomain
{
	[Registry]
	public class InteractionGlobalService:IInteractionGlobalService
	{
		public RegistrableDictionary<int, IInteractionUnitHead> TotalInteractions { get; } = new();

		public RegistrableList<IFreeInteractionUnit> FreeInteractions { get; } = new();

		public IInteractionUnit GenerateFreeInteraction(Role left, Role right)
		{
			InteractionScope.Instance.LeftRole = left;
			InteractionScope.Instance.RightRole = right;
			FreeInteractionBegin freeInteractionBegin = new();
			foreach (IFreeInteractionUnit interaction in 
				FreeInteractions.Get().Where(interaction => interaction.VisibleFor(left,right)))
				freeInteractionBegin.Interactions.Add(
					new Tuple<string, IInteractionUnit>(interaction.Title,interaction));
			
			return freeInteractionBegin;
		}
	}
}