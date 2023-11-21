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
    public class DamageChangeEffect : IInteractionEffect, IHasContent
    {
        [field: S] public float Factor { get; set; }

        public SkillBuff Buff { get; set; }

        public void SetProperty(IBuffProperty property)
        {
        }

        public int Priority => 1;

        public virtual void Active(SkillInteraction target)
        {
            AttackRecordBuff attack = target.GetAttack();
            if (attack is not null)
            {
                attack.DamageFactor += Factor;
            }
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