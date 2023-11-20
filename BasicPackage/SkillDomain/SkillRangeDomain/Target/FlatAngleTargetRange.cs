using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class FlatAngleTargetRange : TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        public override string Description => "平角";

        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition, IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);
            
            return SkillRange.FlatAngle(Start, End, center.Item1, center.Item2);
        }
    }
}