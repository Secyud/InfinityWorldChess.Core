using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.Target
{
    public class AcuteAngleTargetRange : TargetWithoutTetragonalSymmetry,   ISkillCastResult
    {
        protected override string Description => "锐角";
        
        public ISkillRange GetCastResultRange(BattleUnit role, BattleCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);
            
            return SkillRange.AcuteAngle(Start, End,center.Item1,center.Item2 );
        }
    }
}