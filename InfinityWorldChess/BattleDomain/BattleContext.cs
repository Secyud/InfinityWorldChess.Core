using System;
using System.Collections.Generic;
using InfinityWorldChess.BattleDomain.BattleCellDomain;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class BattleContext : IRegistry
    {
        public float TotalTime { get; set; }

        public BattleFlowState State { get; set; } = BattleFlowState.OnCalculation;

        public IReadOnlyList<BattleRole> BattleRoles => Roles;

        internal readonly List<BattleRole> Roles = new();

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

                if (_hoverCell)
                {
                    _hoverCell.Hovered = false;
                }

                _hoverCell = value;

                if (_hoverCell)
                {
                    _hoverCell.Hovered = true;
                }

                MapAction.OnHover(_hoverCell);
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

                if (_selectedCell)
                {
                    _selectedCell.Selected = false;
                }

                _selectedCell = value;
                if (_selectedCell)
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

        private BattleRole _role;

        public BattleRole Role
        {
            get => _role;
            internal set
            {
                if (_role == value)
                {
                    return;
                }

                if (_role is not null)
                {
                    _role.Active = false;
                }

                _role = value;

                if (_role is not null)
                {
                    _role.Active = true;
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

        public event Action ChessRemovedAction;

        public void OnChessRemoved()
        {
            ChessRemovedAction?.Invoke();
        }

        public event Action ChessAddedAction;

        public void OnChessAdded()
        {
            ChessAddedAction?.Invoke();
        }

        #endregion
    }
}