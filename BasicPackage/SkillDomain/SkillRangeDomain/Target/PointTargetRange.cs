using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class PointTargetRange:ISkillCastResult
    {
        public string Description => "单点";
        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition, IActiveSkill skill = null)
        {
            return SkillRange.GetFixedRange(castPosition);
        }
    }
}