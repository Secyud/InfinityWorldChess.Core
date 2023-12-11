using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.BattleBuffFunction
{
    public interface IBuffAttached
    {
        public IBattleUnitBuff Buff { get; set; }
    }
}