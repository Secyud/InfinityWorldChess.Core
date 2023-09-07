using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class PointTargetRange:ISkillCastResult
    {
        public string ShowDescription => "单点";
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition, IActiveSkill skill = null)
        {
            return SkillRange.GetFixedRange(castPosition);
        }
    }
}