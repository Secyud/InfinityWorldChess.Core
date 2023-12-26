using Secyud.Ugf.HexMap;
using Secyud.Ugf.UgfHexMap;

namespace InfinityWorldChess.BattleDomain
{
    public class BattleHexGridDrawer:UgfHexGridDrawer
    {
        public override HexCell CreateCell()
        {
            return new BattleCell();
        }
    }
}