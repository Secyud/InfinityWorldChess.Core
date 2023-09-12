using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using System;
using System.Collections.Generic;
using Secyud.Ugf.AssetComponents;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
	public class FightInteraction
	{
		public string Text => "是吗？就让我领教一下你的武功。";

		public IObjectAccessor<Sprite> Background => 
			SpriteContainer.Create<IwcAssets>("00086-4232632013",prefix:SpritePrefix.Art);


		public IList<Tuple<string, IDialogueUnit>> Selections => null;

		public void OnStart()
		{
		}

		public void OnEnd()
		{
			// Fight fight = new(
			// 	DialogueService.Instance.LeftRole,
			// 	DialogueService.Instance.RightRole)
			// {
			// 	Cell = DialogueService.Instance.RightRole.Position.Cell
			// };
			
			// U.Get<BattleGlobalService>().CreateBattle(fight);
			//
			// U.M.DestroyScope<DialogueService>();
		}

		public int Id => 0;

		public string Title => "切磋";

		public bool VisibleFor(Role left, Role right) => true;
	}
}