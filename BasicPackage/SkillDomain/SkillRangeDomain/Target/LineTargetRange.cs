using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class LineTargetRange : StartEndRange, ISkillCastResult
    {
        public override string ShowDescription => "直线";
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition,IActiveSkill skill)
        {
            return SkillRange.Line(Start, End, castPosition.Coordinates, 
                castPosition.DirectionTo(role.Unit.Location));
        }
    }
}