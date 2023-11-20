using System;
using System.Linq;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class CoreSkillActionService : IBattleMapActionService,IRegistry
    {
        private BattleCell _skillCastCell;
        private CoreSkillContainer _coreSkill;
        private bool _apply;

        public CoreSkillContainer CoreSkill
        {
            get => _coreSkill;
            set
            {
                _coreSkill = value;
                if (_apply)
                {
                    BattleContext context = BattleScope.Instance.Context;
                    BattleRole role = context.Role;
                    context.ReleasableCells = value?.CoreSkill.GetCastPositionRange(role).Value;
                    context.InRangeCells = Array.Empty<BattleCell>();
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
                BattleContext context = BattleScope.Instance.Context;

                if (context.ReleasableCells.Contains(cell))
                {
                    BattleRole role = context.Role;
                    ISkillRange inRange = CoreSkill.Skill.GetCastResultRange(role, cell);
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
            if (CoreSkill is not null)
            {
                _skillCastCell = cell;
                BattleContext context = BattleScope.Instance.Context;
                BattleRole role = context.Role;
                BattleScope.Instance.Map.StartBroadcast(role, cell,
                    CoreSkill.CoreSkill.UnitPlay?.Value);
                MessageScope.Instance.AddMessage(CoreSkill.CoreSkill.Name);
            }
        }

        public void OnTrig()
        {
            BattleContext context = BattleScope.Instance.Context;
            BattleRole role = context.Role;

            role.SetCoreSkillCall((byte)(CoreSkill.EquipCode / 4));

            var selfCell = role.Unit.Location;
            if (selfCell != _skillCastCell)
                role.Direction = selfCell.DirectionTo(_skillCastCell);
            CoreSkill.CoreSkill.ConditionCast(role);
            ISkillRange skillRange =
                CoreSkill.CoreSkill.GetCastResultRange(role, _skillCastCell);
            CoreSkill.CoreSkill.Cast(role, _skillCastCell, skillRange);

            AutoReselectSkill(role);
            
            BattleScope.Instance.Context.OnActionFinished();
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