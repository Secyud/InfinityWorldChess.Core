using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.Target
{
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