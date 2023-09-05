using Secyud.Ugf.TabComponents;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuTabGroup : TabGroup
    {
        protected override TabService Service => GameScope.Instance.Get<GameMenuTabService>();
    }
}