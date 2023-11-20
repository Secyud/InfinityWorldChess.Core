using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class AcuteAngleTargetRange : TargetWithoutTetragonalSymmetry,   ISkillCastResult
    {
        public override string Description => "锐角";
        
        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);
            
            return SkillRange.AcuteAngle(Start, End,center.Item1,center.Item2 );
        }
    }
}