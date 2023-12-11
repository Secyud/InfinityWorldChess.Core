using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class TeammatesTarget : ISkillTargetInRange
    {
        public ISkillTarget GetTargetInRange(BattleUnit battleChess, ISkillRange range)
        {
            return SkillTarget.CreateFromRange(range,
                u => u && u.Camp == battleChess.Camp);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：友军。");
        }
    }
}