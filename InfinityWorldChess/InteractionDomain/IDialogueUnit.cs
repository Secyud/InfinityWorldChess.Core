using Secyud.Ugf;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
	public interface IDialogueUnit
	{
		List<IDialogueAction> ActionList { get; }

		IDialogueAction DefaultAction { get; }
		
		string Text { get; }
	}
}