using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// run an attack.
    /// if not set property yet, use <see cref="AttackWithSetting"/> instead
    /// </summary>
    public class AttackInteraction: ISkillInteractionEffect,IBuffInteractionEffect
    {
        public int Priority => 0x10000;
        public ActiveSkillBase BelongSkill { get; set; }
        public SkillBuff BelongBuff { get; set; }

        public virtual void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标造成伤害。");
        }

        public virtual void Invoke(SkillInteraction interaction)
        {
            BattleRole target = interaction.TargetChess;
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

        public void Active(SkillInteraction target)
        {
            Invoke(target);
        }
    }
}