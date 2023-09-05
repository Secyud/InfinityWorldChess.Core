using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.BattleDomain.BattleCellDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class HoverObservedService:ObservedService
    {
        private BattleCell _hoverCell;
        public BattleCell HoverCell
        {
            get => _hoverCell;
            set
            {
                if (_hoverCell == value)
                    return;
                _hoverCell = value;
                Refresh();
            }
        }
    }
}