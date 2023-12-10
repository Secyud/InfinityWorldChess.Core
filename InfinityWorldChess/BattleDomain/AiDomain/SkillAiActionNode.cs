using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.SkillDomain;
using JetBrains.Annotations;
using Secyud.Ugf;
using Secyud.Ugf.HexUtilities;

namespace InfinityWorldChess.BattleDomain
{
    public class SkillAiActionNode : IAiActionNode
    {
        private readonly BattleCell _cell;
        private readonly SkillContainer _container;
        private readonly BattleRole _battleRole;

        private CoreSkillActionService _coreService;

        private CoreSkillActionService CoreService => _coreService ??= U.Get<CoreSkillActionService>();

        private FormSkillActionService _formService;

        private FormSkillActionService FormService => _formService ??= U.Get<FormSkillActionService>();

        private SkillAiActionNode([NotNull] BattleCell cell,
            [NotNull] SkillContainer container,
            [NotNull] BattleRole battleRole)
        {
            _cell = cell;
            _container = container;
            _battleRole = battleRole;
        }

        public bool IsInterval
        {
            get
            {
                return _container switch
                {
                    CoreSkillContainer => CoreService.IsInterval,
                    FormSkillContainer => FormService.IsInterval,
                    _                  => false
                };
            }
        }

        public  bool InvokeAction()
        {
            switch (_container)
            {
                case CoreSkillContainer coreSkillContainer:
                    CoreService.CoreSkill = coreSkillContainer;
                    CoreService.OnPress(_cell);
                    return true;
                case FormSkillContainer moveSkillContainer:
                    FormService.FormSkill = moveSkillContainer;
                    FormService.OnPress(_cell);
                    return true;
                default:
                    return false;
            }
        }

        public  int GetScore()
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

        public static void AddNodes(List<IAiActionNode> nodes, BattleRole battleRole)
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