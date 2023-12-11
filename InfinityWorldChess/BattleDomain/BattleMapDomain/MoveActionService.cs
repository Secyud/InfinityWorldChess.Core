using System;
using System.Linq;
using InfinityWorldChess.MessageDomain;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class MoveActionService : IBattleMapActionService, IRegistry
    {
        private readonly BattleMapFunctionService _service;
        public bool IsInterval => _service.IsTraveling;

        public MoveActionService(BattleMapFunctionService service)
        {
            _service = service;
        }

        public void OnApply()
        {
            BattleContext context = BattleScope.Instance.Context;
            context.ReleasableCells = context.Unit.GetMoveRange();
        }

        public void OnHover(BattleCell cell)
        {
            BattleContext context = BattleScope.Instance.Context;

            if (context.ReleasableCells.Contains(cell))
            {
                BattleUnit unit = context.Unit;
                _service.FindPath(unit.Location as BattleCell, cell, unit);

                context.InRangeCells = _service.GetPath().Select(BattleScope.Instance.GetCell).ToList();
            }
            else
            {
                context.InRangeCells = Array.Empty<BattleCell>();
            }
        }

        public void OnPress(BattleCell cell)
        {
            BattleContext context = BattleScope.Instance.Context;
            BattleUnit unit = context.Unit;
            unit.ExecutionValue -= unit.GetMoveCast(cell);
            _service.Travel();
            context.StateService.Refresh();
            MessageScope.Instance.AddMessage($"移动至({cell.X},{cell.Z})");
            OnApply();
        }

        public void OnTrig()
        {
        }

        public void OnClear()
        {
            BattleContext context = BattleScope.Instance.Context;
            context.ReleasableCells = Array.Empty<BattleCell>();
            context.InRangeCells = Array.Empty<BattleCell>();
        }
    }
}