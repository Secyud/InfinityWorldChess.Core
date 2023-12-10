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

        public bool IsInterval  => _context.State != BattleFlowState.OnUnitControl;
        
        public FormSkillContainer FormSkill
        {
            get => _formSkill;
            set
            {
                _formSkill = value?.FormSkill.CheckCastCondition(
                    BattleScope.Instance.Context.Role) is not null
                    ? null
                    : value;

                if (_apply)
                {
                    BattleContext context = BattleScope.Instance.Context;
                    BattleRole role = context.Role;
                    context.ReleasableCells = _formSkill?.FormSkill.GetCastPositionRange(role).Value;
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
                    BattleRole role = _context.Role;
                    ISkillRange inRange = FormSkill.Skill.GetCastResultRange(role, cell);
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
                BattleRole role = _context.Role;
                BattleScope.Instance.Map.StartBroadcast(role, cell,
                    FormSkill.FormSkill.UnitPlay?.Value);
                MessageScope.Instance.AddMessage(FormSkill.FormSkill.Name);
            }
        }

        public void OnTrig()
        {
            BattleRole role = _context.Role;

            role.SetFormSkillCall((byte)(FormSkill.EquipCode / 4));

            var selfCell = role.Unit.Location;
            if (selfCell != _skillCastCell)
                role.Direction = selfCell.DirectionTo(_skillCastCell);
            FormSkill.FormSkill.ConditionCast(role);
            ISkillRange skillRange =
                FormSkill.FormSkill.GetCastResultRange(role, _skillCastCell);
            FormSkill.FormSkill.Cast(role, _skillCastCell, skillRange);

            AutoReselectSkill(role);

            _context.OnActionFinished();
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