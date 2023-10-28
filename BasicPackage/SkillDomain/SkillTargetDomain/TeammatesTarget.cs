using System.Linq;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain.SkillTargetDomain
{
    public class TeammatesTarget:ISkillTargetInRange
    {
        public string Description => "友军";
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
    }
}