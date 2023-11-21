using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.Target
{
    public class RoundAngleTargetRange :StartEndRange, ISkillCastResult
    {
        protected override string Description => "圆形";
        
        public ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition,IActiveSkill skill)
        {
            return SkillRange.RoundAngle(Start, End, castPosition.Coordinates);
        }

        public virtual void SetContent(Transform transform)
        {
            transform.AddParagraph("释放目标：" + Description + SeLabel);
        }
    }
}