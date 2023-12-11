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
        private readonly BattleRole _battleRole;
        private MoveActionService _service;

        private MoveActionService Service => _service ??= U.Get<MoveActionService>();

        private MoveAiActionNode(
            [NotNull] BattleCell cell,
            [NotNull] BattleRole battleRole)
        {
            _cell = cell;
            _battleRole = battleRole;
        }

        public bool IsInterval => _service.IsInterval;

        public  bool InvokeAction()
        {
            Service.OnPress(_cell);
            return true;
        }

        public  int GetScore()
        {
            return (int)(from chess in BattleScope.Instance.Context.Roles
                where chess.Camp != _battleRole.Camp 
                let distance1 = Math.Abs(IwcBattleAiController.TargetDistance - chess.Location.DistanceTo(_cell)) 
                let distance2 = Math.Abs(IwcBattleAiController.TargetDistance - chess.DistanceTo(_battleRole)) 
                where distance2 > distance1 select distance2 - distance1).Sum();
        }


        public static void AddNodes(List<IAiActionNode> nodes, BattleRole battleRole)
        {
            IReadOnlyList<BattleCell> range = battleRole.GetMoveRange();

            nodes.AddRange(range.Select(cell => new MoveAiActionNode(cell, battleRole)));
        }
    }
}