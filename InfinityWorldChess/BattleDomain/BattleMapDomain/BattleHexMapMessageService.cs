#region

using InfinityWorldChess.GameDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMapExtensions;
using Secyud.Ugf.HexUtilities;
using Secyud.Ugf.UgfHexMap;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class BattleHexMapMessageService : UgfHexMapMessageService
    {
        public override float GetSpeed(HexUnit unit)
        {
            return 1;
        }

        public override float GetMoveCost(UgfCell fromCell, UgfCell toCell, HexDirection direction)
        {
            if (fromCell.IsValid() && toCell.IsValid())
                return 1;

            return -1;
        }
    }
}