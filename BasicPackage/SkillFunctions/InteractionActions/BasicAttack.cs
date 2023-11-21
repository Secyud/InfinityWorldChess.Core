using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// all attack skill should add attack action to interaction
    /// </summary>
    public class BasicAttack : IInteractionAction
    {
        private const float Pb = 2048f;
        [field: S] public float AttackBaseFactor { get; set; }
        [field: S] public float FixedAttackValue { get; set; }

        public ActiveSkillBase Skill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标造成{AttackBaseFactor:P0}+{FixedAttackValue}点伤害。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            BattleRole target = interaction.TargetChess;

            AttackRecordBuff attackRecord = interaction.GetOrAddAttack();

            attackRecord.AttackFactor += AttackBaseFactor + 4 * (1 - Pb / (Mathf.Max(0, Skill.Kiling) + Pb));
            attackRecord.AttackFixedValue += FixedAttackValue * Skill.Living;
            attackRecord.Attack += interaction.LaunchChess?.AttackValue ?? 0;
            attackRecord.Defend += interaction.TargetChess?.DefendValue ?? 0;
            attackRecord.TargetCount = Skill.Targets.Value.Length;

            if (Skill is not ICoreSkill coreSkill ||
                !coreSkill.FitWeapon(interaction.LaunchChess))
            {
                attackRecord.DamageFactor -= 0.5f;
            }

            float damage = attackRecord.RunDamage(target);

            HexCell cell = target.Unit.Location;

            if (target.HealthValue < 0)
            {
                target.Unit.Die();
            }

            BattleScope.Instance.CreateNumberText(cell, (int)damage, Color.red);
        }
    }
}