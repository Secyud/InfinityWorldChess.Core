using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleCellDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class ObtuseTriangleTargetRange : TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        public override string Description => "钝角";
        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);

            return SkillRange.ObtuseTriangle(Start, End, center.Item1, center.Item2);
        }
    }
}