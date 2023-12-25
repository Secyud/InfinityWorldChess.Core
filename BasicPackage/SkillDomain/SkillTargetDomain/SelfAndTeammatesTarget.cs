using System.Ugf.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class SelfAndTeammatesTarget : ISkillTargetInRange
    {
        public ISkillTarget GetTargetInRange(BattleUnit battleChess, ISkillRange range)
        {
            SkillTarget target = SkillTarget.CreateFromRange(range,
                u => u && u.Camp == battleChess.Camp);
            target.Value.AddIfNotContains(battleChess);
            return target;
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：友军加自身。");
        }
    }
}