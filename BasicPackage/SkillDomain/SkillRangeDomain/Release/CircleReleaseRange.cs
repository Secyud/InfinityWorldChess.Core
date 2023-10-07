using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class CircleReleaseRange :StartEndRange,  ISkillCastPosition
    {
        [field:S] public bool IncludeUnit { get; set; }
        public override string ShowDescription => "圆形";

        public ISkillRange GetCastPositionRange(BattleRole role,IActiveSkill skill)
        {
            return SkillRange.Circle(
                Start, End, role.Unit.Location.Coordinates,IncludeUnit);
        }
    }
}