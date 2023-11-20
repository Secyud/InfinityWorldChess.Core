using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class RoundAngleTargetRange :StartEndRange, ISkillCastResult
    {
        public override string Description => "圆形";
        
        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill)
        {
            return SkillRange.RoundAngle(Start, End, castPosition.Coordinates);
        }
    }
}