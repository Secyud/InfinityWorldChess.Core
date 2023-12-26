using System;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    public class IwcBattleUnitInitializeService : IBattleUnitInitializeService, IRegistry
    {
        public void InitBattleUnit(BattleUnit battleUnit)
        {
            Role role = battleUnit.Role;
            float living = role.BodyPart[BodyType.Living].RealValue + 1;
            float passive = role.PassiveSkill.Living + 1;
            float dif = Math.Abs(living - passive);
            float sum = living + passive;
            float smallFactor = 0x10000 * sum / (dif + 0x200);
            float largeFactor = sum / 0x200;

            float execution = 32 - 32 * dif / sum;

            battleUnit.MaxHealthValue = largeFactor + smallFactor;
            battleUnit.MaxEnergyValue = largeFactor + 0x4 * smallFactor;
            battleUnit.ExecutionValue = execution;
            battleUnit.EnergyRecover = smallFactor / 0x200;
            battleUnit.ExecutionRecover = Math.Max(execution / 4, 1);

            foreach (IActionable<BattleUnit> b in role.Buffs.BattleInitializes)
            {
                b.Invoke(battleUnit);
            }

            battleUnit.HealthValue = battleUnit.MaxHealthValue;
            battleUnit.EnergyValue = battleUnit.MaxEnergyValue;
        }
    }
}