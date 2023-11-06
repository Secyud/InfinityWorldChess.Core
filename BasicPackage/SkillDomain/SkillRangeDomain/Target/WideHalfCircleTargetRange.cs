using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleCellDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class WideHalfCircleTargetRange :TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        public override string Description => "半圆";

        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);

            return SkillRange.WideHalfCircle(Start, End, center.Item1, center.Item2);
        }
    }
}