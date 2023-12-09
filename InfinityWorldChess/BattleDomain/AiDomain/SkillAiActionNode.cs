using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.SkillDomain;
using JetBrains.Annotations;
using Secyud.Ugf;
using Secyud.Ugf.HexUtilities;

namespace InfinityWorldChess.BattleDomain
{
    public class SkillAiActionNode : AiActionNode
    {
        private readonly BattleCell _cell;
        private readonly SkillContainer _container;
        private readonly BattleRole _battleRole;

        private SkillAiActionNode([NotNull] BattleCell cell,
            [NotNull] SkillContainer container,
            [NotNull] BattleRole battleRole)
        {
            _cell = cell;
            _container = container;
            _battleRole = battleRole;
        }

        public override void InvokeAction()
        {
            if (_container is CoreSkillContainer coreSkillContainer)
            {
                CoreSkillActionService skill = U.Get<CoreSkillActionService>();
                skill.CoreSkill = coreSkillContainer;
                skill.OnPress(_cell);
            }
            else if (_container is FormSkillContainer moveSkillContainer)
            {
                FormSkillActionService skill = U.Get<FormSkillActionService>();
                skill.FormSkill = moveSkillContainer;
                skill.OnPress(_cell);
            }
        }

        public override int GetScore()
        {
            ISkillRange range = _container.Skill.GetCastResultRange(_battleRole, _cell);
            ISkillTarget targets = _container.Skill.GetTargetInRange(_battleRole, range);
            float score = 0;
            foreach (BattleRole target in targets.Value)
            {
                float health = target.MaxHealthValue - target.HealthValue;
                score += Math.Min(_container.Skill.Score, 5) + 10 * health / target.MaxHealthValue;
            }
            return Math.Max((int)score, 0);
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
                            .Select(cell => new SkillAiActionNode(cell, skill, battleRole))
                    );
                }
            }

            foreach (FormSkillContainer skill in battleRole.NextFormSkills)
            {
                if (skill is not null &&
                    skill.FormSkill.CheckCastCondition(battleRole) is null)
                {
                    nodes.AddRange(
                        skill.FormSkill.GetCastPositionRange(battleRole).Value
                            .Select(cell => new SkillAiActionNode(cell, skill, battleRole))
                    );
                }
            }
        }
    }
}