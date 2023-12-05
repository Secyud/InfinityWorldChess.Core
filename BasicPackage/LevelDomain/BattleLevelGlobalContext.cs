using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.LevelDomain
{
    public class BattleLevelGlobalContext:IRegistry
    {
        public RegistrableList<IBattleLevel> LevelList { get; } = new();
    }
}