using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillDomain.AttackDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.SkillDomain.SkillTargetDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class BasicAttack : SkillInteractionEffectBase
    {
        protected override ISkillTargetInRange TargetGetter => EnemiesTarget.Instance;
        public override string ShowDescription => $"{p}攻击系数:{AttackBaseFactor}{p}攻击基值:{FixedAttackValue}";

        protected AttackRecordBuff AttackRecord;

        [field: S] public float AttackBaseFactor { get; set; }
        [field: S] public float FixedAttackValue { get; set; }

        protected override void OnInteraction(SkillInteraction interaction)
        {
            if (interaction.TargetChess is ICanDefend defend)
            {
                float damage = AttackRecord.RunDamage(defend);
                HexCell cell = interaction.TargetChess.Unit.Location;
                if (defend.HealthValue < 0)
                    interaction.TargetChess.Unit.Die();

                BattleScope.Instance.CreateNumberText(cell, (int)damage, Color.red);
            }
        }

        protected override void PreInteraction(SkillInteraction interaction)
        {
            AttackRecord = interaction.SetAttack();
            AttackRecord.AttackFactor = AttackBaseFactor + 5.0f - 50f / (Mathf.Max(0, Skill.Kiling) + 10);
            AttackRecord.AttackFixedValue = FixedAttackValue * Skill.Living;
            AttackRecord.Penetration += Skill.Nimble;
            AttackRecord.DamageFactor += Skill.Defend / (100f + Skill.Defend) - 0.5f;
            AttackRecord.TargetCount = Targets.Value.Length;
        }
    }
}