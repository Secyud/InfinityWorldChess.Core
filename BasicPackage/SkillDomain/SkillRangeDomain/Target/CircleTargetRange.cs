using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class CircleTargetRange :StartEndRange, ISkillCastResult
    {
        public override string ShowDescription => "圆形";
        
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition,IActiveSkill skill)
        {
            return SkillRange.Circle(Start, End, castPosition.Coordinates);
        }
    }
}