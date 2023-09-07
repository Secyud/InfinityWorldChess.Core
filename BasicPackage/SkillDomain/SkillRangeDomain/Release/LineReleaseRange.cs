using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class LineReleaseRange : StartEndRange, ISkillCastPosition
    {

        public override string ShowDescription => "直线";

        public ISkillRange GetCastPositionRange(BattleRole role,IActiveSkill skill)
        {
            return SkillRange.Line(
                Start, End, role.Unit.Location.Coordinates, role.Direction);
        }
    }
}