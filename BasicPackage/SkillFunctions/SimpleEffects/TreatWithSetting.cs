using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// all treat skill should add treat action to interaction
    /// </summary>
    public class TreatWithSetting : TreatInteraction
    {
        private const float Pb = 2048f;
        [field: S] public float TreatBaseFactor { get; set; }
        [field: S] public float FixedTreatValue { get; set; }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标造成{TreatBaseFactor:P0}+{FixedTreatValue}点治疗。");
        }

        public override void Invoke(SkillInteraction interaction)
        {
            IBuffProperty property = BelongSkill ?? BelongBuff?.Property;

            if (property is not null)
            {
                TreatRecordBuff treatRecord = interaction.GetOrAddTreat();
                treatRecord.TreatFactor += TreatBaseFactor + 4 *
                    (1 - Pb / (Mathf.Max(0, property.Kiling) + Pb));
                treatRecord.Treat = interaction.LaunchChess?.AttackValue ?? 0;

                if (property is ActiveSkillBase skill)
                {
                    treatRecord.TargetCount = skill.Targets?.Value.Length ?? 0;
                }
            }

            base.Invoke(interaction);
        }
    }
}