using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class AcuteTriangleTargetRange : TargetWithoutTetragonalSymmetry,   ISkillCastResult
    {
        public override string ShowDescription => "锐角";
        
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition,IActiveSkill skill)
        {
            var center = GetCenter(role, castPosition);
            
            return SkillRange.AcuteTriangle(Start, End,center.Item1,center.Item2 );
        }
    }
}