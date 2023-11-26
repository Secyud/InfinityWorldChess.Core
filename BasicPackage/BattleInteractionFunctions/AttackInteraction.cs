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
    public class AttackInteraction : IActionable<BattleInteraction>, IHasContent
    {
        public virtual void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标造成伤害。");
        }

        public virtual void Invoke(BattleInteraction interaction)
        {
            BattleRole target = interaction.Target;
            float damage = interaction
                .GetOrAddAttack()
                .RunDamage(target);

            HexCell cell = target.Unit.Location;

            if (target.HealthValue < 0)
            {
                target.Unit.Die();
            }

            BattleScope.Instance.CreateNumberText(cell, (int)damage, Color.red);
        }
    }
}