using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;

namespace InfinityWorldChess.BattleFunctions
{
    public abstract class BattleActionable:IActionable<BattleScope>
    {
        public abstract void Invoke(BattleScope target);
    }
}