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

        public int Score { get; }

        private CoreSkillActionService _coreService;

        private CoreSkillActionService CoreService => _coreService ??= U.Get<CoreSkillActionService>();

        private FormSkillActionService _formService;

        private FormSkillActionService FormService => _formService ??= U.Get<FormSkillActionService>();


        private SkillAiActionNode(
            [NotNull] SkillContainer container,
            [NotNull] BattleCell cell,
            int score)
        {
            Score = score;
            _cell = cell;
            _container = container;
        }

        public bool InvokeAction()
        {
            IBattleMapActionService service;

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

            BattleScope.Instance.Context.MapAction = service;
            service.OnHover(_cell);
            service.OnPress(_cell);
            return true;
        }

        public static int GetScore(BattleUnit unit, IActiveSkill skill, BattleCell cell)
        {
            ISkillRange range = skill.GetCastResultRange(unit, cell);
            ISkillTarget targets = skill.GetTargetInRange(unit, range);
            float score = 0;
            foreach (BattleUnit target in targets.Value)
            {
                float health = target.MaxHealthValue - target.HealthValue;
                score += Math.Min(skill.Score, 5) + 10 * health / target.MaxHealthValue + 1;
                score += Math.Max(5 - target.DistanceTo(cell), 0);
            }

            return Math.Max((int)score, 0);
        }

        public static void AddNodes(List<IAiActionNode> nodes, BattleUnit unit)
        {
            foreach (CoreSkillContainer skill in unit.NextCoreSkills)
            {
                if (skill is null || skill.CoreSkill.CheckCastCondition(unit) is not null) continue;
                List<BattleCell> range = skill.CoreSkill.GetCastPositionRange(unit).Value;
                nodes.AddRange(
                    from cell in range
                    let score = GetScore(unit, skill.CoreSkill, cell)
                    where score > 0
                    select new SkillAiActionNode(skill, cell, score));
            }
            foreach (FormSkillContainer skill in unit.NextFormSkills)
            {
                if (skill is null || skill.FormSkill.CheckCastCondition(unit) is not null) continue;
                List<BattleCell> range = skill.FormSkill.GetCastPositionRange(unit).Value;
                nodes.AddRange(
                    from cell in range
                    let score = GetScore(unit, skill.FormSkill, cell)
                    where score > 0
                    select new SkillAiActionNode(skill, cell, score));
            }
        }
    }
}