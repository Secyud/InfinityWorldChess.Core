using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface ISkillActionEffect:IHasContent,IActiveSkillAttached
    {
        void Invoke(BattleRole battleChess, BattleCell releasePosition);
    }
}