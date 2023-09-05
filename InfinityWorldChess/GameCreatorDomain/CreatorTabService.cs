using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.TabComponents;

namespace InfinityWorldChess.GameCreatorDomain
{
    [Registry(DependScope=typeof(GameCreatorScope))]
    public class CreatorTabService: TabService
    {
        
    }
}