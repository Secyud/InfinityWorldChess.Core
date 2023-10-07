using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.SkillDomain;
using JetBrains.Annotations;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;

namespace InfinityWorldChess.BattleDomain.AiDomain
{
    public class CoreSkillAiActionNode : AiActionNode
    {
        private readonly HexCell _cell;
        private readonly CoreSkillContainer _container;
        private readonly BattleRole _battleRole;

        private CoreSkillAiActionNode([NotNull] HexCell cell,
            [NotNull] CoreSkillContainer container,
            [NotNull] BattleRole battleRole)
        {
            _cell = cell;
            _container = container;
            _battleRole = battleRole;
        }

        public override void InvokeAction()
        {
            CoreSkillActionService skill = U.Get<CoreSkillActionService>();
            skill.CoreSkill = _container;
            skill.OnPress(_cell);
        }

        public override int GetScore()
        {
            ISkillRange range = _container.Skill.GetCastResultRange(_battleRole, _cell);

            float score = 0;
            foreach (HexCell cell in range.Value)
            {
                if (cell.Unit && cell.Unit.Get<BattleRole>().Camp != _battleRole.Camp)
                {
                    score += score+32;
                }
            }

            HexDirection direction = _cell.DirectionTo(_battleRole.Unit.Location);
            float distanceMin = float.MaxValue;
            float add = float.MaxValue;
            foreach (BattleRole chess in BattleScope.Instance.Context.Roles)
            {
                if (chess == _battleRole)
                    continue;
                float distance = _battleRole.Unit.Location.DistanceTo(chess.Unit.Location);
                if (distance < distanceMin)
                {
                    HexDirection d = chess.Unit.Location.DirectionTo(_battleRole.Unit.Location);

                    int dd = Math.Abs(d - direction);
                    distanceMin = distance;
                    add =Math.Max((1- dd)*2,0) ;
                }
            }

            score += add;
            return Math.Max((int)score,0) ;
        }

        public static void AddNodes(List<AiActionNode> nodes, BattleRole battleRole)
        {
            foreach (CoreSkillContainer skill in battleRole.NextCoreSkills)
            {
                if (skill is not null &&
                    skill.CoreSkill.CheckCastCondition(battleRole) is null)
                {
                    nodes.AddRange(
                        skill.CoreSkill.GetCastPositionRange(battleRole).Value
                            .Select(cell => new CoreSkillAiActionNode(cell, skill, battleRole))
                    );
                }
            }
        }
    }
}