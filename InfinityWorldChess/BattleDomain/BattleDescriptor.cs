namespace InfinityWorldChess.BattleDomain
{
    public abstract class BattleDescriptor
    {
        public BattleCell Cell { get; set; }

        public abstract int SizeX { get; }

        public abstract int SizeZ { get; }
        
        public abstract IBattleVictoryCondition GenerateVictoryCondition();

        public virtual void OnBattleFinished()
        {
        }

        public virtual void OnBattleCreated()
        {
        }
    }
}