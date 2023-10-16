using System;
using System.Linq;
using InfinityWorldChess.BattleDomain.AiDomain;
using InfinityWorldChess.BattleDomain.BattleRoleDomain;
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
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _context.HoverCell = cell;
                if (Input.GetMouseButtonDown(1))
                {
                    _context.SelectCell(cell);
                }
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
            float min = float.MaxValue;
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

            BattleScope.Instance.Map.MapCamera.SetTargetPosition(_context.Role.Unit.Location.Position);

            State = BattleFlowState.OnUnitControl;
        }

        public void ExitControl()
        {
            State = BattleFlowState.OnCalculation;
        }


        private void ControlUnitWithCell(BattleCell cell)
        {
            if (_context.Role.PlayerControl)
            {
                if (cell is null)
                    return;

                if (Input.GetMouseButtonDown(0))
                {
                    _context.TriggerCell(cell);
                }
            }
            else
            {
                switch (_aiController.State)
                {
                    case AiControlState.StartPonder:
                        StartOrContinueAi();
                        break;
                    case AiControlState.InPondering:
                        break;
                    case AiControlState.FinishPonder:
                        _aiController.TryInvokeCurrentNode();
                        break;
                    case AiControlState.NoActionValid:
                        ExitControl();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void StartOrContinueAi()
        {
            BattleScope.Instance.Map.StartCoroutine(_aiController.StartPondering());
        }

        public void TriggerEffect()
        {
            _context.MapAction.OnTrig();
            _context.StateService.Refresh();
            EnterControl();
        }
    }
}