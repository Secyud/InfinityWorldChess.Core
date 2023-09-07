using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class WideHalfCircleReleaseRange : StartEndRange,  ISkillCastPosition,IHasContent
    {
        public override string ShowDescription => "半圆";

        public ISkillRange GetCastPositionRange(BattleRole role)
        {
            return SkillRange.WideHalfCircle(
                Start, End, role.Unit.Location.Coordinates, role.Direction);
        }
    }
}