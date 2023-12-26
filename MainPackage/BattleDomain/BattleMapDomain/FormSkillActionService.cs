using System;
using System.Linq;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class FormSkillActionService : IBattleMapActionService, IRegistry
    {
        private readonly BattleContext _context;
        private BattleCell _skillCastCell;
        private FormSkillContainer _formSkill;
        private bool _apply;

        public FormSkillActionService(BattleContext context)
        {
            _context = context;
        }
        
        public FormSkillContainer FormSkill
        {
            get => _formSkill;
            set
            {
                _formSkill = value?.FormSkill.CheckCastCondition(
                    BattleScope.Instance.Context.Unit) is not null
                    ? null
                    : value;

                if (_apply)
                {
                    BattleContext context = BattleScope.Instance.Context;
                    BattleUnit unit = context.Unit;
                    context.ReleasableCells = _formSkill?.FormSkill.GetCastPositionRange(unit).Value;
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

                if (_context.ReleasableCells.Contains(cell))
                {
                    BattleUnit unit = _context.Unit;
                    ISkillRange inRange = FormSkill.Skill.GetCastResultRange(unit, cell);
                    _context.InRangeCells = inRange.Value;
                }
                else
                {
                    _context.InRangeCells = Array.Empty<BattleCell>();
                }
            }
        }

        public void OnPress(BattleCell cell)
        {
            if (FormSkill is not null)
            {
                _skillCastCell = cell;
                BattleUnit unit = _context.Unit;
                IFormSkill skill = FormSkill.FormSkill;
                BattleScope.Instance.Map.StartBroadcast(unit, cell,
                    skill.UnitPlay?.Instantiate(),skill.EffectDelegate);
                MessageScope.Instance.AddMessage(FormSkill.FormSkill.Name);
            }
        }

        public void OnTrig()
        {
            BattleUnit unit = _context.Unit;

            unit.SetFormSkillCall((byte)(FormSkill.EquipCode / 4));

            var selfCell = unit.Location;
            if (selfCell != _skillCastCell)
                unit.Direction = selfCell.DirectionTo(_skillCastCell);
            FormSkill.FormSkill.ConditionCast(unit);
            ISkillRange skillRange =
                FormSkill.FormSkill.GetCastResultRange(unit, _skillCastCell);
            FormSkill.FormSkill.Cast(unit, _skillCastCell, skillRange);

            AutoReselectSkill(unit);

            _context.OnActionFinished();
        }

        public void AutoReselectSkill(BattleUnit unit)
        {
            FormSkill = unit.NextFormSkills.FirstOrDefault(u => u is not null);
        }

        public void OnClear()
        {
            FormSkill = null;
            _apply = false;
        }
    }
}