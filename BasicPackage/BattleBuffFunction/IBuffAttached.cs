using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.BattleBuffFunction
{
    public interface IBuffAttached
    {
        public IBattleRoleBuff Buff { get; set; }
    }
}