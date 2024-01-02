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
    /// all treat skill should add treat action to interaction
    /// </summary>
    [Guid("21FCF801-AFDC-F99B-ABC5-B1682E02B9DD")]
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
                if (interaction.Origin)
                {
                    treatRecord.Treat = interaction.Origin.AttackValue ;
                }

                if (Property is ActiveSkillBase skill)
                {
                    treatRecord.TargetCount = skill.Targets?.Value.Count ?? 0;
                }
            }

            base.Invoke(interaction);
        }
    }
}