using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleCellDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class AcuteTriangleTargetRange : TargetWithoutTetragonalSymmetry,   ISkillCastResult
    {
        public override string Description => "锐角";
        
        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);
            
            return SkillRange.AcuteTriangle(Start, End,center.Item1,center.Item2 );
        }
    }
}