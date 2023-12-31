using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    /// <summary>
    /// run an attack.
    /// if not set property yet, use <see cref="AttackWithSetting"/> instead
    /// </summary>
    [Guid("54DD327A-C83F-B459-93DD-EA3213E28176")]
    public class AttackInteraction : IActionable<BattleInteraction>, IHasContent
    {
        public virtual void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标造成伤害。");
        }

        public virtual void Invoke(BattleInteraction interaction)
        {
            BattleUnit target = interaction.Target;
            float damage = interaction
                .GetOrAddAttack()
                .RunDamage(target);

            HexCell cell = target.Location;

            if (target.HealthValue < 0)
            {
                BattleScope.Instance.KillBattleUnit(target);
            }

            BattleScope.Instance.CreateNumberText(cell, (int)damage, Color.red);
        }
    }
}