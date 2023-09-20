using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class StateObservedService: ObservedService
    {
        
    }
}