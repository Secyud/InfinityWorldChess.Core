#region

using Secyud.Ugf.HexMap;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    public abstract class BattleDescriptor
    {
        public IHexCell Cell { get; set; }

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