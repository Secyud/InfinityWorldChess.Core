using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.Target
{
    public class PointTargetRange:ISkillCastResult
    {
        public string Description => "单点";
        public ISkillRange GetCastResultRange(BattleUnit role, BattleCell castPosition, IActiveSkill skill = null)
        {
            return SkillRange.GetFixedRange(castPosition);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("释放目标：单点。");
        }
    }
}