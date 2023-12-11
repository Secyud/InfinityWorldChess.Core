using System;
using System.Linq;
using InfinityWorldChess.MessageDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class BattleControlService : IRegistry
    {
        private readonly BattleContext _context;
        private readonly MonoContainer<BattlePlayerController> _playerController;

        protected BattleFlowState State
        {
            get => _context.State;
            set => _context.State = value;
        }

        public BattleControlService(
            BattleContext context,
            IwcAssets assets)
        {
            _context = context;
            _playerController = MonoContainer<BattlePlayerController>.Create(assets);
        }

        public void OnUpdate(BattleCell cell)
        {
            _context.HoverCell = cell;
            if (Input.GetMouseButtonDown(1))
            {
                _context.SelectCell(cell);
            }

            switch (State)
            {
                case BattleFlowState.AnimationPlay:
                    break;
                case BattleFlowState.OnCalculation:
                    CalculateTurn();
                    break;
                case BattleFlowState.OnUnitControl:
                    ControlUnitWithCell(cell);
                    break;
                case BattleFlowState.OnEffectTrig:
                    TriggerEffect();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CalculateTurn()
        {
            BattleScope.Instance.Context.OnRoundEnd();

            BattleRole battleRole =
                _context.Roles.First(u => !u.Dead);
            int min = int.MaxValue;
            foreach (BattleRole r in _context.Roles)
            {
                if (!r.Dead && r.Time < min)
                {
                    battleRole = r;
                    min = r.Time;
                }
            }

            battleRole.Time += battleRole.GetTimeAdd();
            battleRole.ExecutionValue += battleRole.ExecutionRecover;
            battleRole.EnergyValue += battleRole.EnergyRecover;
            BattleScope.Instance.Context.TotalTime = min;

            _context.Unit = battleRole;

            BattleScope.Instance.Context.OnRoundBegin();

            _context.StateService.Refresh();
            _context.RoleService.Refresh();
            _context.SelectedCellService.Refresh();
            _context.HoverCellService.Refresh();

            EnterControl();
        }

        public void EnterControl()
        {
            MessageScope.Instance.AddMessage($"【{_context.Unit.Role.ShowName}】回合");
            BattleScope.Instance.Map.MapCamera.SetTargetPosition(_context.Unit.Location.Position);
            State = BattleFlowState.OnUnitControl;

            if (_context.Unit.PlayerControl)
            {
                _playerController.Create();
            }
            else
            {
                _playerController.Destroy();
                TryPondering();
            }
        }

        public void ExitControl()
        {
            CalculateTurn();
        }

        private IBattleAiController _controller;

        private void TryPondering()
        {
            _controller ??= U.Get<IBattleAiController>();
            _controller.TryPondering();
        }


        private void ControlUnitWithCell(BattleCell cell)
        {
            if (_context.Unit.PlayerControl)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _context.TriggerCell(cell);
                }
            }
            else
            {
                TryPondering();
            }
        }

        public void TriggerEffect()
        {
            _context.MapAction.OnTrig();
            _context.StateService.Refresh();
            EnterControl();
        }
    }
}