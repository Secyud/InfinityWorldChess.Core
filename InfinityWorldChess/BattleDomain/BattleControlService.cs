using System;
using System.Linq;
using InfinityWorldChess.MessageDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class BattleControlService : IRegistry
    {
        private readonly BattleContext _context;
        private readonly IBattleAiController _aiController;
        private readonly MonoContainer<BattlePlayerController> _playerController;

        protected BattleFlowState State
        {
            get => _context.State;
            set => _context.State = value;
        }

        public BattleControlService(
            BattleContext context,
            IBattleAiController aiController,
            IwcAssets assets)
        {
            _context = context;
            _aiController = aiController;
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
            battleRole.ExecutionValue += battleRole.ExecutionRecoverValue;
            battleRole.EnergyValue += battleRole.EnergyRecoverValue;
            BattleScope.Instance.Context.TotalTime = min;

            _context.Role = battleRole;

            BattleScope.Instance.Context.OnRoundBegin();

            _context.StateService.Refresh();
            _context.RoleService.Refresh();
            _context.SelectedCellService.Refresh();
            _context.HoverCellService.Refresh();

            EnterControl();
        }

        public void EnterControl()
        {
            if (_context.Role.PlayerControl)
            {
                _playerController.Create();
            }
            else
            {
                _playerController.Destroy();
                StartOrContinueAi();
            }

            MessageScope.Instance.AddMessage($"进入【{_context.Role.Role.ShowName}】回合");
            
            BattleScope.Instance.Map.MapCamera.SetTargetPosition(_context.Role.Unit.Location.Position);

            State = BattleFlowState.OnUnitControl;
        }

        public void ExitControl()
        {
            //State = BattleFlowState.OnCalculation;
            CalculateTurn();
        }


        private void ControlUnitWithCell(BattleCell cell)
        {
            if (_context.Role.PlayerControl)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _context.TriggerCell(cell);
                }
            }
            else
            {
                StartOrContinueAi();
            }
        }

        public void StartOrContinueAi()
        {
            _aiController.StartPondering();
        }

        public void TriggerEffect()
        {
            _context.MapAction.OnTrig();
            _context.StateService.Refresh();
            EnterControl();
        }
    }
}