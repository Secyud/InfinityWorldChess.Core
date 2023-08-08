using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.RefreshComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.BattleCellDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class HoverRefreshService:RefreshService<HoverRefreshService,HoverRefreshItem> 
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