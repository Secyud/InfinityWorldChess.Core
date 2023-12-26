using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;

namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleDescriptor:IShowable,IDataResource
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