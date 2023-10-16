using InfinityWorldChess.GameDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.UgfHexMap;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class BattleMapFunctionService:
        UgfHexMapFunctionService<BattleHexMapMessageService>
    {
        public BattleMapFunctionService(BattleHexMapMessageService service) : base(service)
        {
        }
    }
}