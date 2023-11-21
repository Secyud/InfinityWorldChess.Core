using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface ISkillTargetInRange:IHasContent
    {
        ISkillTarget GetTargetInRange(BattleRole battleChess, ISkillRange range);
    }
}