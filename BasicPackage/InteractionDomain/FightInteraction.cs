using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
	public class FightInteraction:IFreeInteractionUnit
	{
		public string Text => "是吗？就让我领教一下你的武功。";

		public IObjectAccessor<Sprite> Background => 
			SpriteContainer.Create<IwcAb>("00086-4232632013",prefix:SpritePrefix.Art);


		public IList<Tuple<string, IInteractionUnit>> Selections => null;

		public void OnStart()
		{
		}

		public void OnEnd()
		{
			Fight fight = new(
				InteractionScope.Context.LeftRole,
				InteractionScope.Context.RightRole)
			{
				Cell = InteractionScope.Context.RightRole.Position.Cell
			};
			fight.OnBattleCreate();
			Og.ScopeFactory.DestroyScope<InteractionScope>();
		}

		public int Id => 0;

		public string Title => "切磋";

		public bool VisibleFor(Role left, Role right) => true;
	}
}