using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.Target
{
    [Guid("7623C6D9-D946-A969-CE81-B7E225956231")]
    public class LineTargetRange : TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        protected override string Description => "直线";

        public ISkillRange GetCastResultRange(BattleUnit role, BattleCell castPosition, IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);
            return SkillRange.Line(Start, End, center.Item1, center.Item2);
        }
    }
}