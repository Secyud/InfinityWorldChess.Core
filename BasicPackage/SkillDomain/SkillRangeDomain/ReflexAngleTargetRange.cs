﻿using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("23A51C9F-3519-F2CC-5CBD-6C601CF15AF9")]
    public class ReflexAngleTargetRange :TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        protected override string Description => "半圆";

        public ISkillRange GetCastResultRange(BattleUnit role, BattleCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);

            return SkillRange.ReflexAngle(Start, End, center.Item1, center.Item2);
        }
    }
}