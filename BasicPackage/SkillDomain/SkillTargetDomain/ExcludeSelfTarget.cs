using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class ExcludeSelfTarget:ISkillTargetInRange
    {
        public ISkillTarget GetTargetInRange(BattleRole battleChess, ISkillRange range)
        {
            return new SkillTarget(
                from cell in range.Value
                where cell.Unit
                select cell.Unit.Get<BattleRole>()
            );
        }
        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：除自身外所有人。");
        }
    }
}