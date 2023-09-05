using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillTargetDomain
{
    public class SelfAndTeammatesTarget : ISkillTargetInRange
    {
        public string ShowDescription => "友军加自身";
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
    }
}