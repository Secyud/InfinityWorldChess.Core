using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.SkillDomain;
using JetBrains.Annotations;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public class SkillAiActionNode : IAiActionNode
    {
        private readonly BattleCell _cell;
        private readonly SkillContainer _container;
        private readonly BattleUnit _battleUnit;

        private CoreSkillActionService _coreService;

        private CoreSkillActionService CoreService => _coreService ??= U.Get<CoreSkillActionService>();

        private FormSkillActionService _formService;

        private FormSkillActionService FormService => _formService ??= U.Get<FormSkillActionService>();

        
        private SkillAiActionNode([NotNull] BattleCell cell,
            [NotNull] SkillContainer container,
            [NotNull] BattleUnit battleUnit)
        {
            _cell = cell;
            _container = container;
            _battleUnit = battleUnit;
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
            IBattleMapActionService service = null;
            
            switch (_container)
            {
                case CoreSkillContainer coreSkillContainer:
                    CoreService.CoreSkill = coreSkillContainer;
                    service = CoreService;
                    break;
                case FormSkillContainer moveSkillContainer:
                    FormService.FormSkill = moveSkillContainer;
                    service = FormService;
                    break;
                default:
                    return false;
            }
            service.OnApply();
            service.OnHover(_cell);
            service.OnPress(_cell);
            return true;
        }

        public  int GetScore()
        {
            ISkillRange range = _container.Skill.GetCastResultRange(_battleUnit, _cell);
            ISkillTarget targets = _container.Skill.GetTargetInRange(_battleUnit, range);
            float score = 0;
            foreach (BattleUnit target in targets.Value)
            {
                float health = target.MaxHealthValue - target.HealthValue;
                score += Math.Min(_container.Skill.Score, 5) + 10 * health / target.MaxHealthValue;
            }
            return Math.Max((int)score, 0);
        }

        public static void AddNodes(List<IAiActionNode> nodes, BattleUnit battleUnit)
        {
            foreach (CoreSkillContainer skill in battleUnit.NextCoreSkills)
            {
                if (skill is not null &&
                    skill.CoreSkill.CheckCastCondition(battleUnit) is null)
                {
                    nodes.AddRange(
                        skill.CoreSkill.GetCastPositionRange(battleUnit).Value
                            .Select(cell => new SkillAiActionNode(cell, skill, battleUnit))
                    );
                }
            }

            foreach (FormSkillContainer skill in battleUnit.NextFormSkills)
            {
                if (skill is not null &&
                    skill.FormSkill.CheckCastCondition(battleUnit) is null)
                {
                    nodes.AddRange(
                        skill.FormSkill.GetCastPositionRange(battleUnit).Value
                            .Select(cell => new SkillAiActionNode(cell, skill, battleUnit))
                    );
                }
            }
        }
    }
}