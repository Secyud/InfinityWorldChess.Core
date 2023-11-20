using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class RoundAngleReleaseRange :StartEndRange,  ISkillCastPosition
    {
        [field:S] public bool IncludeUnit { get; set; }
        public override string Description => "圆形";

        public ISkillRange GetCastPositionRange(BattleRole role,IActiveSkill skill)
        {
            return SkillRange.RoundAngle(
                Start, End, role.Unit.Location.Coordinates,IncludeUnit);
        }
    }
}