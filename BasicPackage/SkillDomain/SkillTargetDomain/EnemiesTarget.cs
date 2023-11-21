using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class EnemiesTarget:ISkillTargetInRange
    {
        public static EnemiesTarget Instance { get; } = new();
        
        public ISkillTarget GetTargetInRange(BattleRole battleChess, ISkillRange range)
        {
            return new SkillTarget(
                from cell in range.Value
                where cell.Unit
                select cell.Unit.Get<BattleRole>()
                into chess
                where chess?.Camp != battleChess.Camp
                select chess
            );
        }
        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：敌方。");
        }
    }
}