using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("D034B75A-B4AC-163C-E1FE-7223DA9C5767")]
    public class PointTargetRange:ISkillCastResult,IHasDescription
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