using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleCellDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class CircleTargetRange :StartEndRange, ISkillCastResult
    {
        public override string Description => "圆形";
        
        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill)
        {
            return SkillRange.Circle(Start, End, castPosition.Coordinates);
        }
    }
}