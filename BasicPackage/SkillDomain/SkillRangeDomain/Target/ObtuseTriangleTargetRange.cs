using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class ObtuseTriangleTargetRange : TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        public override string ShowDescription => "钝角";
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);

            return SkillRange.WideTriangle(Start, End, center.Item1, center.Item2);
        }
    }
}