using Secyud.Ugf;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
	public interface IInteractionUnit
	{
		IObjectAccessor<Sprite> Background { get; }
		IList<Tuple<string, IInteractionUnit>> Selections { get; }
		string Text { get; }
		void OnStart();
		void OnEnd();
	}
}