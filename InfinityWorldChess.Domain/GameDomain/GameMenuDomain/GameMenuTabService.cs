using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.TabComponents;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class GameMenuTabService: TabService<GameMenuTabService,GameMenuTabItem>
    {
        
    }
}