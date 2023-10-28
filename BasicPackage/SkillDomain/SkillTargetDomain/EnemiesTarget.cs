using System.Linq;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillTargetDomain
{
    public class EnemiesTarget:ISkillTargetInRange
    {
        public static EnemiesTarget Instance { get; } = new();
        
        public string Description => "敌方";
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
    }
}