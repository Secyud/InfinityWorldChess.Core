using System.Runtime.InteropServices;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    /// <summary>
    /// all attack skill should add attack action to interaction
    /// </summary>
    [Guid("976e4a11-19d0-3847-d162-c18a7ecc48cf")]
    public class AttackWithSetting : AttackInteraction,IPropertyAttached
    {
        private const float Pb = 2048f;
        [field: S] public float AttackBaseFactor { get; set; }
        [field: S] public float FixedAttackValue { get; set; }

        public IAttachProperty Property { get; set; }
        
        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标造成{AttackBaseFactor:P0}+{FixedAttackValue}点伤害。");
        }

        public override void Invoke(BattleInteraction interaction)
        {
            AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
            if (interaction.Origin)
            {
                attackRecord.Attack += interaction.Origin.AttackValue;
            }

            if (interaction.Target)
            {
                attackRecord.Defend += interaction.Target.DefendValue;
            }

            if (Property is not null)
            {
                attackRecord.AttackFactor += AttackBaseFactor + 4 * (1 - Pb / (Mathf.Max(0, Property.Kiling) + Pb));
                attackRecord.AttackFixedValue += FixedAttackValue * Property.Living;

                if (Property is ActiveSkillBase skillBase)
                {
                    attackRecord.TargetCount = skillBase.Targets?.Value.Count ?? 0;
                }

                if (Property is IActiveSkill activeSkill &&
                    !activeSkill.FitWeapon(interaction.Origin))
                {
                    attackRecord.DamageFactor += 0.5f;
                }
            }

            base. Invoke(interaction);
        }
    }
}