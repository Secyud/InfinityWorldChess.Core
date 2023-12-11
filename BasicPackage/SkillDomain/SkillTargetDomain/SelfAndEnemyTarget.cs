using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class SelfAndEnemyTarget : ISkillTargetInRange
    {
        public ISkillTarget GetTargetInRange(BattleRole battleChess, ISkillRange range)
        {
            return SkillTarget.CreateFromRange(range,
                c => c && (c.Camp != battleChess.Camp || c == battleChess));
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：敌人及自身。");
        }
    }
}