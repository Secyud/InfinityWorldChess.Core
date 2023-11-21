using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class SelfAndTeammatesTarget : ISkillTargetInRange
    {
        public ISkillTarget GetTargetInRange(BattleRole battleChess, ISkillRange range)
        {
            List<BattleRole> r =
                (from cell in range.Value
                    where cell.Unit
                    select cell.Unit.Get<BattleRole>()
                    into c
                    where c?.Camp == battleChess.Camp
                    select c).ToList();
            r.AddIfNotContains(battleChess);
            return new SkillTarget(r);
        }
        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：友军加自身。");
        }
    }
}