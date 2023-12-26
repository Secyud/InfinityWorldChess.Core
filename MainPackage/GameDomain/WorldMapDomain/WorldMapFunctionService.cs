using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.UgfHexMap;

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class WorldMapFunctionService:UgfHexMapFunctionService<WorldHexMapMessageService>
    {
        public WorldMapFunctionService(WorldHexMapMessageService service) : base(service)
        {
        }
    }
}