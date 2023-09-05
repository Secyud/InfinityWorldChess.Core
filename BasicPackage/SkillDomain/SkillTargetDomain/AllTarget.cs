using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillTargetDomain
{
    public class AllTarget : ISkillTargetInRange
    {
        public string ShowDescription => "所有人";

        public ISkillTarget GetTargetInRange(BattleRole battleChess, ISkillRange range)
        {
            List<BattleRole> r =
                (from cell in range.Value
                    where cell.Unit
                    select cell.Unit.Get<BattleRole>())
                .ToList();
            return new SkillTarget(r);
        }
    }
}