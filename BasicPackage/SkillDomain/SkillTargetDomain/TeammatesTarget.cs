using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class TeammatesTarget : ISkillTargetInRange
    {
        public ISkillTarget GetTargetInRange(BattleRole battleChess, ISkillRange range)
        {
            return new SkillTarget(
                from cell in range.Value
                where cell.Unit
                select cell.Unit.Get<BattleRole>()
                into c
                where c?.Camp == battleChess.Camp
                select c
            );
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：友军。");
        }
    }
}