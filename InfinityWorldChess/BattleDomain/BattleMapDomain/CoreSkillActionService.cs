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
                    BattleRole role = _context.Unit;
                    _context.ReleasableCells = _coreSkill?.CoreSkill
                        .GetCastPositionRange(role).Value;
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
                    BattleRole role = _context.Unit;
                    ISkillRange inRange = CoreSkill.Skill.GetCastResultRange(role, cell);
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
                BattleRole role = _context.Unit;
                BattleScope.Instance.Map.StartBroadcast(role, cell,
                    CoreSkill.CoreSkill.UnitPlay?.Value);
                MessageScope.Instance.AddMessage(CoreSkill.CoreSkill.Name);
            }
        }

        public void OnTrig()
        {
            BattleRole role = _context.Unit;

            role.SetCoreSkillCall((byte)(CoreSkill.EquipCode / 4));

            var selfCell = role.Location;
            if (selfCell != _skillCastCell)
                role.Direction = selfCell.DirectionTo(_skillCastCell);
            CoreSkill.CoreSkill.ConditionCast(role);
            ISkillRange skillRange =
                CoreSkill.CoreSkill.GetCastResultRange(role, _skillCastCell);
            CoreSkill.CoreSkill.Cast(role, _skillCastCell, skillRange);

            AutoReselectSkill(role);

            _context.OnActionFinished();
        }

        public void AutoReselectSkill(BattleRole role)
        {
            CoreSkill = role.NextCoreSkills.FirstOrDefault(u => u is not null);
        }

        public void OnClear()
        {
            CoreSkill = null;
            _apply = false;
        }
    }
}