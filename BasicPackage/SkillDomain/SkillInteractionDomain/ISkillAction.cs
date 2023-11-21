using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface ISkillAction:IHasContent,IActiveSkillAttached
    {
        void Invoke(BattleRole battleChess, BattleCell releasePosition);
    }
}