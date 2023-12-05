using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.LevelDomain
{
    public interface IBattleLevel:IBattleDescriptor
    {
        public int Level { get; }

    }
}