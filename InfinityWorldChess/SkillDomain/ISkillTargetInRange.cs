using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface ISkillTargetInRange:IHasDescription
    {
        ISkillTarget GetTargetInRange(BattleRole battleChess, ISkillRange range);
    }
}