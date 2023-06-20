using Secyud.Ugf;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
	public class FreeInteractionBegin:IInteractionUnitHead
	{
		public string Text => "你找我作甚？";

		public IObjectAccessor<Sprite> Background => null;


		public List<Tuple<string, IInteractionUnit>> Interactions { get; } = new();

		public IList<Tuple<string, IInteractionUnit>> Selections => Interactions;

		public void OnStart()
		{
		}

		public void OnEnd()
		{
			
		}

		public int Id => 0;
	}
}