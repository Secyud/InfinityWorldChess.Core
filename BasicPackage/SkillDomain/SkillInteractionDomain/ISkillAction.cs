using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface ISkillAction:IHasContent,ISkillAttached
    {
        void Invoke(BattleRole battleChess, BattleCell releasePosition);
    }
}