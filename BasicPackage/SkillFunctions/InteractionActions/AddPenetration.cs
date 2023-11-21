using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class AddPenetration : IInteractionAction
    {
        [field: S] public float Value { get; set; }
        [field: S] public float Factor { get; set; }

        public ActiveSkillBase Skill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式增加{Value}+{Factor:P0}[杀]点穿透。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
            attackRecord.Penetration += Value + Factor * Skill.Kiling;
        }
    }
}