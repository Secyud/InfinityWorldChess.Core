using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class ObtuseTriangleReleaseRange : StartEndRange, ISkillCastPosition
    {
        public override string ShowDescription => "钝角";

        public ISkillRange GetCastPositionRange(BattleRole role,IActiveSkill skill)
        {
            return SkillRange.ObtuseTriangle(
                Start, End, role.Unit.Location.Coordinates, role.Direction);
        }
    }
}