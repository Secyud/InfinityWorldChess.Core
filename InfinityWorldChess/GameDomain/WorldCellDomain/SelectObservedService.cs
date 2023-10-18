using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMapExtensions;
using Secyud.Ugf.ObserverComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class SelectObservedService : ObservedService
    {
        private WorldCell _cell;

        public WorldCell Cell
        {
            get => _cell;
            internal set
            {
                if (_cell == value) return;
                if (_cell)
                {
                    _cell.SetHighlight();
                }

                _cell = value;
                if (_cell)
                {
                    _cell.EnableHighlight(Color.green);
                }

                Refresh();
            }
        }

        private WorldCell _hoverCell;

        public WorldCell HoverCell
        {
            get => _hoverCell;
            set
            {
                if (_hoverCell == value) return;

                if (_hoverCell)
                {
                    if (_hoverCell == _cell)
                    {
                        _hoverCell.EnableHighlight(Color.green);
                    }
                    else
                    {
                        _hoverCell.SetHighlight();
                    }
                }

                _hoverCell = value;
                _hoverCell.EnableHighlight(Color.white);
            }
        }
    }
}