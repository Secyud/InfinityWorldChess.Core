using Secyud.Ugf.TabComponents;

namespace InfinityWorldChess.InteractionDomain
{
    public class InteractionTabGroup: TabGroup
    {
        protected override TabService Service => InteractionScope.Instance.Get<InteractionTabService>();
    }
}