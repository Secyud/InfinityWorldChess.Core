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

        private MoveAiActionNode(
            [NotNull] BattleCell cell,
            [NotNull] BattleUnit battleUnit)
        {
            _cell = cell;
            _battleUnit = battleUnit;
        }

        public bool IsInterval => _service.IsInterval;

        public  bool InvokeAction()
        {
            Service.OnApply();
            Service.OnHover(_cell);
            Service.OnPress(_cell);
            return true;
        }

        public  int GetScore()
        {
            return (int)(from chess in BattleScope.Instance.Context.Units
                where chess.Camp != _battleUnit.Camp 
                let distance1 = Math.Abs(IwcBattleAiController.TargetDistance - chess.Location.DistanceTo(_cell)) 
                let distance2 = Math.Abs(IwcBattleAiController.TargetDistance - chess.DistanceTo(_battleUnit)) 
                where distance2 > distance1 select distance2 - distance1).Sum();
        }


        public static void AddNodes(List<IAiActionNode> nodes, BattleUnit battleUnit)
        {
            IReadOnlyList<BattleCell> range = battleUnit.GetMoveRange();

            nodes.AddRange(range.Select(cell => new MoveAiActionNode(cell, battleUnit)));
        }
    }
}