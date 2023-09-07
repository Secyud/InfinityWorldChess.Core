using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class ObtuseTriangleTargetRange : TargetWithoutTetragonalSymmetry, ISkillCastResult,IHasContent
    {
        public override string ShowDescription => "钝角";
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition)
        {
            var center = GetCenter(role, castPosition);

            return SkillRange.WideTriangle(Start, End, center.Item1, center.Item2);
        }
    }
}