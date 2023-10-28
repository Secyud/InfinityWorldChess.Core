using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class LineTargetRange : StartEndRange, ISkillCastResult
    {
        public override string Description => "直线";
        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill)
        {
            return SkillRange.Line(Start, End, castPosition.Coordinates, 
                castPosition.DirectionTo(role.Unit.Location));
        }
    }
}