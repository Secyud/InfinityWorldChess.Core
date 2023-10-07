using System;
using System.Collections.Generic;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
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

        private HexCell _hoverCell;

        public HexCell HoverCell
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
                    _hoverCell.Get<BattleCell>().Hovered = false;
                }

                _hoverCell = value;

                if (_hoverCell)
                {
                    _hoverCell.Get<BattleCell>().Hovered = true;
                }

                MapAction.OnHover(_hoverCell);
                HoverCellService.Refresh();
            }
        }

        public ObservedService HoverCellService { get; } = new();

        private HexCell _selectedCell;

        public HexCell SelectedCell
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
                    _selectedCell.Get<BattleCell>().Selected = false;
                }

                _selectedCell = value;
                if (_selectedCell)
                {
                    _selectedCell.Get<BattleCell>().Selected = true;
                }

                SelectedCellService.Refresh();
            }
        }

        public ObservedService SelectedCellService { get; } = new();


        private readonly List<HexCell> _releasableCells = new();

        public IReadOnlyList<HexCell> ReleasableCells
        {
            get => _releasableCells;
            set
            {
                foreach (HexCell cell in _releasableCells)
                {
                    cell.Get<BattleCell>().Releasable = false;
                }

                _releasableCells.Clear();
                if (value is null) return;

                foreach (HexCell cell in value)
                {
                    cell.Get<BattleCell>().Releasable = true;
                    _releasableCells.Add(cell);
                }
            }
        }

        private readonly List<HexCell> _inRangeCells = new();

        public IReadOnlyList<HexCell> InRangeCells
        {
            get => _inRangeCells;
            set
            {
                foreach (HexCell cell in _inRangeCells)
                {
                    cell.Get<BattleCell>().InRange = false;
                }

                _inRangeCells.Clear();
                if (value is null) return;

                foreach (HexCell cell in value)
                {
                    cell.Get<BattleCell>().InRange = true;
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

        public virtual void SelectCell(HexCell cell)
        {
            SelectedCell = cell;
        }

        public virtual void TriggerCell(HexCell cell)
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