using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class WideHalfCircleTargetRange :TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        public override string ShowDescription => "半圆";

        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);

            return SkillRange.WideHalfCircle(Start, End, center.Item1, center.Item2);
        }
    }
}