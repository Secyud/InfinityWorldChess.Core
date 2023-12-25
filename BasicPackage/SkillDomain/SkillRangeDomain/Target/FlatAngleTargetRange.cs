using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.Target
{
    [Guid("88B15AF6-490C-D5BC-32EE-F7E88BFA67F0")]
    public class FlatAngleTargetRange : TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        protected override string Description => "平角";

        public ISkillRange GetCastResultRange(BattleUnit role, BattleCell castPosition, IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);
            
            return SkillRange.FlatAngle(Start, End, center.Item1, center.Item2);
        }
    }
}