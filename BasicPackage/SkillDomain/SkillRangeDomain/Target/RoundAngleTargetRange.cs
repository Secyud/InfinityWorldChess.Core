using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.Target
{
    [Guid("62E639E2-F85B-3E56-8FAE-9D1CE26E7490")]
    public class RoundAngleTargetRange :StartEndRange, ISkillCastResult
    {
        protected override string Description => "圆形";
        
        public ISkillRange GetCastResultRange(BattleUnit role, BattleCell castPosition,IActiveSkill skill)
        {
            return SkillRange.RoundAngle(Start, End, castPosition.Coordinates);
        }

        public virtual void SetContent(Transform transform)
        {
            transform.AddParagraph("释放目标：" + Description + SeLabel);
        }
    }
}