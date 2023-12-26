using System.Collections.Generic;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.DialogueDomain
{
	public interface IDialogueUnit
	{
		// for the selection of dialogue
		IList<DialogueOption> OptionList { get; }

		// for default continue chat action
		IActionable DefaultAction { get; }
		
		// the saying text for current role
		string Text { get; }
		
		IObjectAccessor<Role> RoleAccessor { get; }
	}
}