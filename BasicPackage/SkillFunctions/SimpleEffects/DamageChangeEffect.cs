#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.SkillFunctions
{
    public class DamageChangeEffect : IBuffInteractionEffect, ISkillInteractionEffect
    {
        [field: S] public float Factor { get; set; }

        public SkillBuff BelongBuff { get; set; }
        public ActiveSkillBase BelongSkill { get; set; }

        public int Priority => 1;


        public void Invoke(SkillInteraction interaction)
        {
            AttackRecordBuff attack = interaction.GetAttack();
            if (attack is not null)
            {
                attack.DamageFactor += Factor;
            }
        }
        public virtual void Active(SkillInteraction target)
        {
            Invoke(target);
        }

        public void SetContent(Transform transform)
        {
            if (Factor > 0)
            {
                transform.AddParagraph($"伤害增加{Factor:P0}。");
            }
            else if (Factor < 0)
            {
                transform.AddParagraph($"伤害减少{-Factor:P0}。");
            }
        }
    }
}