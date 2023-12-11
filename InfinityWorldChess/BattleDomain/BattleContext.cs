using System;
using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class BattleContext : IRegistry
    {
        public int TotalTime { get; set; }

        public BattleFlowState State { get; set; } = BattleFlowState.OnCalculation;

        public List<BattleUnit> Units { get; } = new();

        #region State

        private BattleCell _hoverCell;

        public BattleCell HoverCell
        {
            get => _hoverCell;
            set
            {
                if (_hoverCell == value)
                {
                    return;
                }

                if (_hoverCell is not null )
                {
                    _hoverCell.Hovered = false;
                }

                _hoverCell = value;

                if (_hoverCell is not null )
                {
                    _hoverCell.Hovered = true;
                }

                MapAction?.OnHover(_hoverCell);
                HoverCellService.Refresh();
            }
        }

        public ObservedService HoverCellService { get; } = new();

        private BattleCell _selectedCell;

        public BattleCell SelectedCell
        {
            get => _selectedCell;
            set
            {
                if (_selectedCell == value)
                {
                    return;
                }

                if (_selectedCell is not null )
                {
                    _selectedCell.Selected = false;
                }

                _selectedCell = value;
                if (_selectedCell is not null )
                {
                    _selectedCell.Selected = true;
                }

                SelectedCellService.Refresh();
            }
        }

        public ObservedService SelectedCellService { get; } = new();


        private readonly List<BattleCell> _releasableCells = new();

        public IReadOnlyList<BattleCell> ReleasableCells
        {
            get => _releasableCells;
            set
            {
                foreach (BattleCell cell in _releasableCells)
                {
                    cell.Releasable = false;
                }

                _releasableCells.Clear();
                if (value is null) return;

                foreach (BattleCell cell in value)
                {
                    cell.Releasable = true;
                    _releasableCells.Add(cell);
                }
            }
        }

        private readonly List<BattleCell> _inRangeCells = new();

        public IReadOnlyList<BattleCell> InRangeCells
        {
            get => _inRangeCells;
            set
            {
                foreach (BattleCell cell in _inRangeCells)
                {
                    cell.InRange = false;
                }

                _inRangeCells.Clear();
                if (value is null) return;

                foreach (BattleCell cell in value)
                {
                    cell.InRange = true;
                    _inRangeCells.Add(cell);
                }
            }
        }

        private IBattleMapActionService _mapAction;

        public IBattleMapActionService MapAction
        {
            get => _mapAction;
            set
            {
                if (_mapAction == value)
                {
                    return;
                }

                _mapAction?.OnClear();
                _mapAction = value;
                _mapAction?.OnApply();
            }
        }

        private BattleUnit _unit;

        public BattleUnit Unit
        {
            get => _unit;
            internal set
            {
                if (_unit == value)
                {
                    return;
                }

                if (_unit is not null)
                {
                    _unit.Active = false;
                }

                _unit = value;

                if (_unit is not null)
                {
                    _unit.Active = true;
                }

                RoleService.Refresh();
            }
        }

        public ObservedService RoleService { get; } = new();
        public ObservedService StateService { get; } = new();

        public virtual void SelectCell(BattleCell cell)
        {
            SelectedCell = cell;
        }

        public virtual void TriggerCell(BattleCell cell)
        {
            if (_releasableCells.Contains(cell))
            {
                MapAction?.OnPress(cell);
            }
        }

        #endregion

        #region Actions

        public event Action BattleFinishAction;

        public void OnBattleFinished()
        {
            BattleFinishAction?.Invoke();
        }

        public event Action ActionFinishedAction;

        public void OnActionFinished()
        {
            ActionFinishedAction?.Invoke();
        }

        public event Action RoundEndAction;

        public void OnRoundEnd()
        {
            RoundEndAction?.Invoke();
        }

        public event Action RoundBeginAction;

        public void OnRoundBegin()
        {
            RoundBeginAction?.Invoke();
        }

        public event Action<BattleUnit> ChessRemoveAction;

        public void OnChessRemove(BattleUnit unit)
        {
            ChessRemoveAction?.Invoke(unit);
        }

        public event Action<BattleUnit> ChessAddAction;

        public void OnChessAdd(BattleUnit unit)
        {
            ChessAddAction?.Invoke(unit);
        }

        #endregion
    }
}