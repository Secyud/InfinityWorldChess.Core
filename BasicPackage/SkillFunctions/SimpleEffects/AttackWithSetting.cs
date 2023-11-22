using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// all attack skill should add attack action to interaction
    /// </summary>
    [ID("F1807254-E937-3ED1-9C8F-218EB2EB7075")]
    public class AttackWithSetting : AttackInteraction
    {
        private const float Pb = 2048f;
        [field: S] public float AttackBaseFactor { get; set; }
        [field: S] public float FixedAttackValue { get; set; }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标造成{AttackBaseFactor:P0}+{FixedAttackValue}点伤害。");
        }

        public override void Invoke(SkillInteraction interaction)
        {
            IBuffProperty property = BelongSkill ?? BelongBuff?.Property;

            AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
            attackRecord.Attack = interaction.LaunchChess?.AttackValue ?? 0;
            attackRecord.Defend = interaction.TargetChess?.DefendValue ?? 0;
            
            if (property is not null)
            {
                attackRecord.AttackFactor += AttackBaseFactor + 4 * (1 - Pb / (Mathf.Max(0, property.Kiling) + Pb));
                attackRecord.AttackFixedValue += FixedAttackValue * property.Living;
                
                if (property is ActiveSkillBase skillBase)
                {
                    attackRecord.TargetCount = skillBase.Targets?.Value.Length ?? 0;
                }

                if (property is not ICoreSkill coreSkill ||
                    !coreSkill.FitWeapon(interaction.LaunchChess))
                {
                    attackRecord.DamageFactor -= 0.5f;
                }
            }
            
            base.Invoke(interaction);
        }
    }
}