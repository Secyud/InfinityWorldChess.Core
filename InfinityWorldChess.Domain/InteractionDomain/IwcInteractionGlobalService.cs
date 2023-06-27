using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Collections;
using System;
using System.Linq;

namespace InfinityWorldChess.InteractionDomain
{
	public class IwcInteractionGlobalService:IInteractionGlobalService
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