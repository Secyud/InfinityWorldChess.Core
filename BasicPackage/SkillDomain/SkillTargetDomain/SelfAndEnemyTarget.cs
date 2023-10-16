using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillTargetDomain
{
    public class SelfAndEnemyTarget : ISkillTargetInRange
    {
        public string ShowDescription => "敌人及自身";
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
    }
}