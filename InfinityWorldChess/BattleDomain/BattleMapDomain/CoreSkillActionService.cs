using System;
using System.Linq;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class CoreSkillActionService : IBattleMapActionService, IRegistry
    {
        private readonly BattleContext _context;
        private BattleCell _skillCastCell;
        private CoreSkillContainer _coreSkill;
        private bool _apply;

        public CoreSkillActionService(BattleContext context)
        {
            _context = context;
        }

        public bool IsInterval => _context.State != BattleFlowState.OnUnitControl;

        public CoreSkillContainer CoreSkill
        {
            get => _coreSkill;
            set
            {
                _coreSkill = value?.CoreSkill
                    .CheckCastCondition(_context.Unit) is not null ? null : value;

                if (_apply)
                {
                    BattleUnit unit = _context.Unit;
                    _context.ReleasableCells = _coreSkill?.CoreSkill
                        .GetCastPositionRange(unit).Value;
                    _context.InRangeCells = Array.Empty<BattleCell>();
                }
            }
        }

        public void OnApply()
        {
            _apply = true;
            CoreSkill = CoreSkill;
        }

        public void OnHover(BattleCell cell)
        {
            if (CoreSkill is not null)
            {
                if (_context.ReleasableCells.Contains(cell))
                {
                    BattleUnit unit = _context.Unit;
                    ISkillRange inRange = CoreSkill.Skill.GetCastResultRange(unit, cell);
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
            if (CoreSkill is not null)
            {
                _skillCastCell = cell;
                BattleUnit unit = _context.Unit;
                BattleScope.Instance.Map.StartBroadcast(unit, cell,
                    CoreSkill.CoreSkill.UnitPlay?.Value);
                MessageScope.Instance.AddMessage(CoreSkill.CoreSkill.Name);
            }
        }

        public void OnTrig()
        {
            BattleUnit unit = _context.Unit;

            unit.SetCoreSkillCall((byte)(CoreSkill.EquipCode / 4));

            var selfCell = unit.Location;
            if (selfCell != _skillCastCell)
                unit.Direction = selfCell.DirectionTo(_skillCastCell);
            CoreSkill.CoreSkill.ConditionCast(unit);
            ISkillRange skillRange =
                CoreSkill.CoreSkill.GetCastResultRange(unit, _skillCastCell);
            CoreSkill.CoreSkill.Cast(unit, _skillCastCell, skillRange);

            AutoReselectSkill(unit);

            _context.OnActionFinished();
        }

        public void AutoReselectSkill(BattleUnit unit)
        {
            CoreSkill = unit.NextCoreSkills.FirstOrDefault(u => u is not null);
        }

        public void OnClear()
        {
            CoreSkill = null;
            _apply = false;
        }
    }
}