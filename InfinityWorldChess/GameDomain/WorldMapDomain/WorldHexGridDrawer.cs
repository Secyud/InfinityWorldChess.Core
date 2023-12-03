using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.UgfHexMap;

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    public class WorldHexGridDrawer:UgfHexGridDrawer
    {
        public override HexCell CreateCell()
        {
            return new WorldCell();
        }
    }
}