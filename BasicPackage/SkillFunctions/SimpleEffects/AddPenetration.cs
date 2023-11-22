using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class AddPenetration : ISkillInteractionEffect, IBuffInteractionEffect
    {
        [field: S] public float Value { get; set; }
        [field: S] public float Factor { get; set; }

        public ActiveSkillBase BelongSkill { get; set; }
        public SkillBuff BelongBuff { get; set; }
        public int Priority => 0;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式增加{Value}+{Factor:P0}[杀]点穿透。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            var property = BelongSkill ?? BelongBuff?.Property;
            if (property is not null)
            {
                AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
                attackRecord.Penetration += Value + Factor * BelongSkill.Kiling;
            }
        }

        public void Active(SkillInteraction target)
        {
            Invoke(target);
        }
    }
}