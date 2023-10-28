using System.Linq;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillTargetDomain
{
    public class ExcludeSelfTarget:ISkillTargetInRange
    {
        public string Description => "除自身外所有人";
        public ISkillTarget GetTargetInRange(BattleRole battleChess, ISkillRange range)
        {
            return new SkillTarget(
                from cell in range.Value
                where cell.Unit
                select cell.Unit.Get<BattleRole>()
            );
        }
    }
}