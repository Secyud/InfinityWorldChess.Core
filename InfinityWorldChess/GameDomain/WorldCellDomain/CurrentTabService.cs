using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.TabComponents;

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class CurrentTabService : TabService
    {
        private WorldCell _cell;

        public WorldCell Cell
        {
            get => _cell;
            internal set
            {
                if (_cell == value)
                {
                    return;
                }

                _cell = value;
                RefreshCurrentTab();
            }
        }
    }
}