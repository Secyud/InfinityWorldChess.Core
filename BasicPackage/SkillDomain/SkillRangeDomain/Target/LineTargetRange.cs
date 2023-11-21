using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.Target
{
    public class LineTargetRange : TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        protected override string Description => "直线";

        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition, IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);
            return SkillRange.Line(Start, End, center.Item1, center.Item2);
        }
    }
}