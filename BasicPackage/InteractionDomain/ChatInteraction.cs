using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using System;
using System.Collections.Generic;
using Secyud.Ugf.AssetComponents;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
	public class ChatInteraction:IFreeInteractionUnit
	{
		public string Text => "今天天气真好啊。";

		public IObjectAccessor<Sprite> Background => 
			SpriteContainer.Create<IwcAb>("00015-3099803928",prefix:SpritePrefix.Art);

		public IList<Tuple<string, IInteractionUnit>> Selections => null;

		public void OnStart()
		{
		}

		public void OnEnd()
		{
		}

		public int Id => 0;


		public string Title => "闲聊";

		public bool VisibleFor(Role left, Role right) => true;
	}
}