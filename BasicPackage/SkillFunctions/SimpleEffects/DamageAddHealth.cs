using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// 吸血
    /// </summary>
    public class DamageAddHealth : ISkillInteractionEffect,IBuffInteractionEffect
    {
        [field: S] private float Factor { get; set; }

        public ActiveSkillBase BelongSkill { get; set; }   
        public SkillBuff BelongBuff { get; set; }
        public int Priority => 0x20000;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式将造成伤害的{Factor:P0}转化为自身生命。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            AttackRecordBuff attackRecord = interaction.GetAttack();
            if (attackRecord is not null)
            {
                TreatRecordBuff treatRecord = interaction.GetOrAddTreat();
                treatRecord.Treat = attackRecord.TotalDamage * Factor;
                treatRecord.RunRecover(interaction.LaunchChess);
            }
        }
        public void Active(SkillInteraction target)
        {
            Invoke(target);
        }

    }
}