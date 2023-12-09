using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.SkillDomain;
using JetBrains.Annotations;
using Secyud.Ugf;
using Secyud.Ugf.HexMapExtensions;

namespace InfinityWorldChess.BattleDomain
{
    public class MoveAiActionNode : AiActionNode
    {
        private readonly BattleCell _cell;
        private readonly BattleRole _battleRole;

        private MoveAiActionNode(
            [NotNull] BattleCell cell,
            [NotNull] BattleRole battleRole)
        {
            _cell = cell;
            _battleRole = battleRole;
        }

        public override void InvokeAction()
        {
            MoveActionService service = U.Get<MoveActionService>();
            service.OnPress(_cell);
        }

        public override int GetScore()
        {
            return (int)(from chess in BattleScope.Instance.Context.Roles
                where chess.Camp != _battleRole.Camp 
                let distance1 = Math.Abs(IwcBattleAiController.TargetDistance - chess.Unit.Location.DistanceTo(_cell)) 
                let distance2 = Math.Abs(IwcBattleAiController.TargetDistance - chess.Unit.DistanceTo(_battleRole.Unit)) 
                where distance2 > distance1 select distance2 - distance1).Sum();
        }


        public static void AddNodes(List<AiActionNode> nodes, BattleRole battleRole)
        {
            IReadOnlyList<BattleCell> range = battleRole.GetMoveRange();

            nodes.AddRange(range.Select(cell => new MoveAiActionNode(cell, battleRole)));
        }
    }
}