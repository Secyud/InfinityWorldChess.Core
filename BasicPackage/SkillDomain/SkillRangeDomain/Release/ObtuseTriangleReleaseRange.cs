using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class ObtuseTriangleReleaseRange : StartEndRange, ISkillCastPosition,IHasContent
    {
        public override string ShowDescription => "钝角";

        public ISkillRange GetCastPositionRange(BattleRole role)
        {
            return SkillRange.WideTriangle(
                Start, End, role.Unit.Location.Coordinates, role.Direction);
        }
    }
}