using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain.BattleCellDomain;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.SkillDomain;
using JetBrains.Annotations;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BattleDomain.AiDomain
{
    public class FormSkillAiActionNode : AiActionNode
    {
        private readonly BattleCell _cell;
        private readonly FormSkillContainer _container;
        private readonly BattleRole _battleRole;

        private FormSkillAiActionNode(
            [NotNull] BattleCell cell,
            [NotNull] FormSkillContainer container,
            [NotNull] BattleRole battleRole)
        {
            _cell = cell;
            _container = container;
            _battleRole = battleRole;
        }

        public override void InvokeAction()
        {
            FormSkillActionService skill = U.Get<FormSkillActionService>();
            skill.FormSkill = _container;
            skill.OnPress(_cell);
        }

        public override int GetScore()
        {
            float minDistance1 = float.MaxValue;
            float minDistance2 = float.MaxValue;
            foreach (BattleRole chess in BattleScope.Instance.Context.Roles)
            {
                if (chess == _battleRole)
                    continue;
                float distance1 = chess.Unit.Location.DistanceTo(_cell);
                float distance2 = chess.Unit.Location.DistanceTo(_battleRole.Unit.Location);
                if (chess.Camp != _battleRole.Camp)
                {
                    if (minDistance1 > distance1)
                        minDistance1 = distance1;
                    if (minDistance2 > distance2)
                        minDistance2 = distance2;
                }
            }

            if (minDistance2 <=Math.Max(2,minDistance1) )
            {
                return 0;
            }

            return Math.Max(32-(int)Math.Abs(minDistance2 - minDistance1-2), 0);
        }


        public static void AddNodes(List<AiActionNode> nodes, BattleRole battleRole)
        {
            foreach (FormSkillContainer skill in battleRole.NextFormSkills)
            {
                if (skill is not null &&
                    skill.FormSkill.CheckCastCondition(battleRole) is null)
                {
                    nodes.AddRange(
                        skill.FormSkill.GetCastPositionRange(battleRole).Value
                            .Select(cell => new FormSkillAiActionNode(cell, skill, battleRole))
                    );
                }
            }
        }
    }
}