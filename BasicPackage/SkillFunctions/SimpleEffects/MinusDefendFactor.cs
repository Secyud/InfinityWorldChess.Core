using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class MinusDefendFactor : ISkillInteractionEffect,IBuffInteractionEffect
    {
        [field: S] public float Factor { get; set; }

        public int Priority => 0;
        public ActiveSkillBase BelongSkill { get; set; }
        public SkillBuff BelongBuff { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此攻击无视敌方{Factor:P0}防御。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
            attackRecord.DefendFactor -= Factor;
        }

        public void Active(SkillInteraction target)
        {
            Invoke(target);
        }
    }
}