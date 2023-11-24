using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleRoleFunctions
{
    /// <summary>
    /// all treat skill should add treat action to interaction
    /// </summary>
    public class TreatWithSetting : TreatInteraction,IPropertyAttached
    {
        private const float Pb = 2048f;
        [field: S] public float TreatBaseFactor { get; set; }
        [field: S] public float FixedTreatValue { get; set; }

        public IAttachProperty Property { get; set; }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标造成{TreatBaseFactor:P0}+{FixedTreatValue}点治疗。");
        }

        public override void Invoke(BattleInteraction interaction)
        {
            if (Property is not null)
            {
                TreatRecordProperty treatRecord = interaction.GetOrAddTreat();
                treatRecord.TreatFactor += TreatBaseFactor + 4 *
                    (1 - Pb / (Mathf.Max(0, Property.Kiling) + Pb));
                treatRecord.Treat = interaction.Origin?.AttackValue ?? 0;

                if (Property is ActiveSkillBase skill)
                {
                    treatRecord.TargetCount = skill.Targets?.Value.Length ?? 0;
                }
            }

            base.Invoke(interaction);
        }
    }
}