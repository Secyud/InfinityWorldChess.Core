#region

using InfinityWorldChess.GameDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.UgfHexMap;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class BattleHexMapMessageService : UgfHexMapMessageService
    {
        public override float GetSpeed(HexUnit unit)
        {
            BattleRole role = BattleScope.Instance.GetChess(unit);
            if (role is null) return 1;

            return role.GetSpeed();
        }
    }
}