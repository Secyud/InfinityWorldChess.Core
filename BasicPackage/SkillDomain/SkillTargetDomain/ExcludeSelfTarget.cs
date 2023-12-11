using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class ExcludeSelfTarget:ISkillTargetInRange
    {
        public ISkillTarget GetTargetInRange(BattleUnit battleChess, ISkillRange range)
        {
            return SkillTarget.CreateFromRange(range,
                u => u && u != battleChess);
        }
        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：除自身外所有人。");
        }
    }
}