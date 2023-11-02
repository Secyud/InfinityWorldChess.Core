using System.Collections.Generic;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.DialogueDomain
{
	public interface IDialogueUnit
	{
		// for the selection of dialogue
		IList<IDialogueAction> ActionList { get; }

		// for default continue chat action
		IDialogueAction DefaultAction { get; }
		
		// the saying text for current role
		string Text { get; }
		
		IObjectAccessor<Role> RoleAccessor { get; }
	}
}