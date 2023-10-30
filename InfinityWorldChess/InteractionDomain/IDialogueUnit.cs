using System.Collections.Generic;

namespace InfinityWorldChess.InteractionDomain
{
	public interface IDialogueUnit
	{
		// for the selection of dialogue
		List<IDialogueAction> ActionList { get; }

		// for default continue chat action
		IDialogueAction DefaultAction { get; }
		
		// the saying text for current role
		string Text { get; }
		 int RoleId { get; }
	}
}