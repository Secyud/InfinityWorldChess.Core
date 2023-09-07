﻿using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class WideHalfCircleReleaseRange : StartEndRange,  ISkillCastPosition
    {
        public override string ShowDescription => "半圆";

        public ISkillRange GetCastPositionRange(BattleRole role,IActiveSkill skill)
        {
            return SkillRange.WideHalfCircle(
                Start, End, role.Unit.Location.Coordinates, role.Direction);
        }
    }
}