using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class ReflexAngleTargetRange :TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        public override string Description => "半圆";

        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);

            return SkillRange.ReflexAngle(Start, End, center.Item1, center.Item2);
        }
    }
}