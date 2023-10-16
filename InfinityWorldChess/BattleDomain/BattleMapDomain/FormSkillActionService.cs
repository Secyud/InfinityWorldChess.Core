using System;
using System.Linq;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain.BattleMapDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class FormSkillActionService: IBattleMapActionService,IRegistry
    {
        private BattleCell _skillCastCell;
        private FormSkillContainer _formSkill;
        private bool _apply;

        public FormSkillContainer FormSkill
        {
            get => _formSkill;
            set
            {
                _formSkill = value;
                if (_apply)
                {
                    BattleContext context = BattleScope.Instance.Context;
                    BattleRole role = context.Role;
                    context.ReleasableCells = value?.FormSkill.GetCastPositionRange(role).Value;
                    context.InRangeCells = Array.Empty<BattleCell>();
                }
            }
        }

        public void OnApply()
        {
            _apply = true;
            FormSkill = FormSkill;
        }
        
        public void OnHover(BattleCell cell)
        {
            if (FormSkill is not null)
            {
                BattleContext context = BattleScope.Instance.Context;

                if (context.ReleasableCells.Contains(cell))
                {
                    BattleRole role = context.Role;
                    ISkillRange inRange = FormSkill.Skill.GetCastResultRange(role, cell);
                    context.InRangeCells = inRange.Value;
                }
                else
                {
                    context.InRangeCells = Array.Empty<BattleCell>();
                }
            }
        }

        public void OnPress(BattleCell cell)
        {
            if (FormSkill is not null)
            {
                _skillCastCell = cell;
                BattleContext context = BattleScope.Instance.Context;
                BattleRole role = context.Role;
                BattleScope.Instance.Map.StartBroadcast(role, cell,
                    FormSkill.FormSkill.UnitPlay?.Value);
            }
        }

        public void OnTrig()
        {
            BattleContext context = BattleScope.Instance.Context;
            BattleRole role = context.Role;

            role.SetFormSkillCall((byte)(FormSkill.EquipCode / 4));

            BattleCell selfCell = role.Unit.Location as BattleCell;
            if (selfCell != _skillCastCell)
                role.Direction = _skillCastCell.DirectionTo(selfCell);
            FormSkill.FormSkill.ConditionCast(role);
            ISkillRange skillRange =
                FormSkill.FormSkill.GetCastResultRange(role, _skillCastCell);
            FormSkill.FormSkill.Cast(role, _skillCastCell, skillRange);

            AutoReselectSkill(role);
            
            BattleScope.Instance.Context.OnActionFinished();
        }

        public void AutoReselectSkill(BattleRole role)
        {
            FormSkill = role.NextFormSkills.FirstOrDefault(u => u is not null);
        }
        public void OnClear()
        {
            FormSkill = null;
            _apply = false;
        }
    }
}