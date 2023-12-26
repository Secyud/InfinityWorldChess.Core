using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("1361F508-3E9E-4A40-A077-938B411131B5")]
    public class ObtuseAngleTargetRange : TargetWithoutTetragonalSymmetry, ISkillCastResult
    {
        protected override string Description => "钝角";
        public ISkillRange GetCastResultRange(BattleUnit role, BattleCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);

            return SkillRange.ObtuseAngle(Start, End, center.Item1, center.Item2);
        }
    }
}