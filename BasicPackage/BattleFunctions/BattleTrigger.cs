using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;

namespace InfinityWorldChess.BattleFunctions
{
    public abstract class BattleTrigger:ITrigger<BattleScope>
    {
        public abstract void Invoke(BattleScope target);
    }
}