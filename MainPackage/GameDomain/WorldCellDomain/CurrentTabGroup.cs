using Secyud.Ugf.TabComponents;

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class CurrentTabGroup :TabGroup
    {
        protected override TabService Service => GameScope.Instance.Get<CurrentTabService>();
    }
}