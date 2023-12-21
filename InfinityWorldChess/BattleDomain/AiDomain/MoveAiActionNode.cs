using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public class MoveAiActionNode : IAiActionNode
    {
        private readonly BattleCell _cell;
        private readonly BattleUnit _battleUnit;
        private MoveActionService _service;

        private MoveActionService Service => _service ??= U.Get<MoveActionService>();


        public int Score { get; }
        
        private MoveAiActionNode(
            [NotNull] BattleCell cell,
            [NotNull] BattleUnit battleUnit,
            int score)
        {
            _cell = cell;
            _battleUnit = battleUnit;
            Score = score;
        }

        public bool IsInterval => _service.IsInterval;

        public  bool InvokeAction()
        {
            BattleScope.Instance.Context.MapAction = Service;
            Service.OnHover(_cell);
            Service.OnPress(_cell);
            return true;
        }

        private static int GetScore(BattleUnit unit,BattleCell cell)
        {
            return (int)(from chess in BattleScope.Instance.Context.Units
                where chess.Camp != unit.Camp 
                let distance1 = Math.Abs(IwcBattleAiController.TargetDistance - chess.Location.DistanceTo(cell)) 
                let distance2 = Math.Abs(IwcBattleAiController.TargetDistance - chess.DistanceTo(unit)) 
                where distance2 > distance1 select distance2 - distance1).Sum();
        }

        public static void AddNodes(List<IAiActionNode> nodes, BattleUnit unit)
        {
            IReadOnlyList<BattleCell> range = unit.GetMoveRange();

            nodes.AddRange(
                from cell in range
                let score = GetScore(unit, cell) 
                where score > 0 
                select new MoveAiActionNode(cell, unit, score));
        }
    }
}