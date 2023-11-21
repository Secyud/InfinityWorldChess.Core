using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// 吸血
    /// </summary>
    public class DamageAddHealth : IInteractionAction
    {
        [field: S] private float Factor { get; set; }

        public ActiveSkillBase Skill { get; set; }

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
                treatRecord.RunRecover(interaction.LaunchChess);
            }
        }
    }
}