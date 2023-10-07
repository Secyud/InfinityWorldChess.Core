using System;
using System.Linq;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BattleDomain.BattleMapDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class CoreSkillActionService : IBattleMapActionService,IRegistry
    {
        public CoreSkillContainer CoreSkill { get; set; }
        private HexCell _skillCastCell;

        public void OnHover(HexCell cell)
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
                    context.InRangeCells = Array.Empty<HexCell>();
                }
            }
        }

        public void OnPress(HexCell cell)
        {
            if (CoreSkill is not null)
            {
                _skillCastCell = cell;
                BattleContext context = BattleScope.Instance.Context;
                BattleRole role = context.Role;
                BattleScope.Instance.Map.StartBroadcast(role, cell,
                    CoreSkill.CoreSkill.UnitPlay?.Value);
            }
        }

        public void OnTrig()
        {
            BattleContext context = BattleScope.Instance.Context;
            BattleRole role = context.Role;

            role.SetCoreSkillCall((byte)(CoreSkill.EquipCode / 4));

            HexCell selfCell = role.Unit.Location;
            if (selfCell != _skillCastCell)
                role.Direction = _skillCastCell.DirectionTo(selfCell);
            CoreSkill.CoreSkill.ConditionCast(role);
            ISkillRange skillRange =
                CoreSkill.CoreSkill.GetCastResultRange(role, _skillCastCell);
            CoreSkill.CoreSkill.Cast(role, _skillCastCell, skillRange);

            AutoReselectSkill(role);
            
            BattleScope.Instance.Context.OnActionFinished();
        }

        public void OnClear()
        {
            CoreSkill = null;
        }

        public void AutoReselectSkill(BattleRole role)
        {
            CoreSkill = role.NextCoreSkills.FirstOrDefault(u => u is not null);
        }
    }
}