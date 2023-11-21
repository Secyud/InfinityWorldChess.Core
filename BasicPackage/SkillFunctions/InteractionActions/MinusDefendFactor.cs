using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class MinusDefendFactor : IInteractionAction
    {
        [field: S] public float Factor { get; set; }

        public ActiveSkillBase Skill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式无视敌方{Factor:P0}防御。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
            attackRecord.DefendFactor -= Factor;
        }
    }
}