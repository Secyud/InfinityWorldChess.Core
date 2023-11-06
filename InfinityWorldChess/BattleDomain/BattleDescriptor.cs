using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleDescriptor:IShowable
    {
        WorldCell Cell { get; }

        int SizeX { get; }

        int SizeZ { get; }

        void OnBattleFinished();

        void OnBattleCreated();
		
        public bool Victory { get; }
		
        public bool Defeated { get; }
    }
}