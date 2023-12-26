using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("1E08CF53-E380-88A0-B82F-83560527A095")]
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