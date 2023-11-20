using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class ObtuseAngleTargetRange : TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        public override string Description => "钝角";
        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);

            return SkillRange.ObtuseAngle(Start, End, center.Item1, center.Item2);
        }
    }
}