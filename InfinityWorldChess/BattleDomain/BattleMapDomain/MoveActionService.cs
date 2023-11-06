using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain.BattleCellDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.BattleMapDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class MoveActionService : IBattleMapActionService, IRegistry
    {
        private readonly BattleMapFunctionService _service;

        public MoveActionService(BattleMapFunctionService service)
        {
            _service = service;
        }
        
        public void OnApply()
        {
            BattleContext context = BattleScope.Instance.Context;
            BattleRole role = context.Role;
            context.ReleasableCells = GetRange(role);
        }

        public void OnHover(BattleCell cell)
        {
            BattleContext context = BattleScope.Instance.Context;

            if (context.ReleasableCells.Contains(cell))
            {
                HexUnit unit = context.Role.Unit;
                _service.FindPath(unit.Location as BattleCell, 
                    cell, unit);
                context.InRangeCells = _service.GetPath()
                    .Cast<BattleCell>().ToList();
            }
            else
            {
                context.InRangeCells = Array.Empty<BattleCell>();
            }
        }

        public void OnPress(BattleCell cell)
        {
            BattleContext context = BattleScope.Instance.Context;
            BattleRole role = context.Role;
            HexUnit unit = context.Role.Unit;
            float distance = cell.DistanceTo(unit.Location);
            role.ExecutionValue -= (int)(distance / role.Role.BodyPart.Nimble.RealValue * 100);
            _service.Travel();
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

        private IReadOnlyList<BattleCell> GetRange(BattleRole role)
        {
            if (role.ExecutionValue <= 0)
            {
                return Array.Empty<BattleCell>();
            }
            
            RoleBodyPart nimble = role.Role.BodyPart.Nimble;
            int execution = role.ExecutionValue;

            byte rg = (byte)Math.Min(nimble.RealValue * execution / 100, 10);

            HexGrid grid = BattleScope.Instance.Map;
            List<BattleCell> cells = new();
            List<Vector2> checks = new();
            HexCoordinates coordinate = role.Unit.Location.Coordinates;

            for (int i = 1; i < rg; i++)
            {
                HexCoordinates tmp = coordinate;

                for (int j = 0; j < 6; j++)
                {
                    HexDirection direction = (HexDirection)(j % 6);
                    for (int k = 0; k < i; k++)
                    {
                        TryAddCell(tmp);
                        tmp += direction;
                    }
                }

                TryAddCell(tmp);
            }

            return cells;

            void TryAddCell(HexCoordinates c)
            {
                BattleCell cell = grid.GetCell(c) as BattleCell;

                if (!cell) return;

                Vector2 p2d = (c - role.Unit.Location.Coordinates).Position2D();

                const float r2 = 1.5f;
                foreach (Vector2 check in checks)
                {
                    if (check.x * p2d.x < 0 ||
                        check.y * p2d.y < 0)
                        continue;
                    float a = check.x * p2d.y - p2d.x * check.y;
                    float d = a * a / (check.x * check.x + check.y * check.y);
                    if (d < r2)
                        return;
                }

                if (cell.Unit)
                {
                    checks.Add(p2d);
                    return;
                }

                cells.Add(cell);
            }
        }
    }
}