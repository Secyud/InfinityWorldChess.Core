﻿using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;

namespace InfinityWorldChess.SkillEffectDomain.BasicMoveBundle
{
	public class FlashEffect : ISkillCastEffect
	{
		public string ShowDescription => "闪现至目标点";
		public void Cast(BattleRole role, HexCell releasePosition, ISkillRange range)
		{
			HexDirection direction = releasePosition.DirectionTo(role.Unit.Location);
			role.Unit.Location = releasePosition;
			role.Direction = direction;
		}
	}
}