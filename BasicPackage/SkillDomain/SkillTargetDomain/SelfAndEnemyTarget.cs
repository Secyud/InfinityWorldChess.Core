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
            List<BattleRole> r =
                (from cell in range.Value
                    where cell.Unit
                    select cell.Unit.Get<BattleRole>()
                    into c
                    where c?.Camp != battleChess.Camp || c == battleChess
                    select c).ToList();
            return new SkillTarget(r);
        }
        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：敌人及自身。");
        }
    }
}