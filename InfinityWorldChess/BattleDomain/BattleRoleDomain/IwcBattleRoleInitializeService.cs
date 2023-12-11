using System;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    public class IwcBattleRoleInitializeService : IBattleRoleInitializeService, IRegistry
    {
        public void InitBattleRole(BattleRole battleRole)
        {
            Role role = battleRole.Role;
            float living = role.BodyPart[BodyType.Living].RealValue + 1;
            float passive = role.PassiveSkill.Living + 1;
            float dif = Math.Abs(living - passive);
            float sum = living + passive;
            float smallFactor = 0x10000 * sum / (dif + 0x200);
            float largeFactor = sum / 0x200;

            float execution = 32 - 32 * dif / sum;

            battleRole.MaxHealthValue = largeFactor + smallFactor;
            battleRole.MaxEnergyValue = largeFactor + 0x4 * smallFactor;
            battleRole.ExecutionValue = execution;
            battleRole.EnergyRecover = 0x4 * smallFactor;
            battleRole.ExecutionRecover = Math.Max(execution / 4, 1);

            foreach (IActionable<BattleRole> b in role.Buffs.BattleInitializes)
            {
                b.Invoke(battleRole);
            }

            battleRole.HealthValue = battleRole.MaxHealthValue;
            battleRole.EnergyValue = battleRole.MaxEnergyValue;
        }
    }
}