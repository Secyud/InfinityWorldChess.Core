using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class AttackEffect : IActionableEffect, IHasContent
    {
        [field: S] public float AttackValue { get; set; }
        [field: S] public float AttackFactor { get; set; }
        [field: S] public byte AttackType { get; set; }

        // to avoid self call
        private bool _triggerState;

        public SkillBuff Buff { get; set; }

        public void SetProperty(IBuffProperty property)
        {
        }

        public void Active()
        {
            if (_triggerState)
                return;

            _triggerState = true;

            SkillInteraction interaction = SkillInteraction.Create(null, Buff.Role);

            AttackRecordBuff attack = interaction.GetOrAddAttack();
            attack.AttackType = (AttackType)AttackType;
            attack.Attack = AttackValue;
            attack.AttackFactor += AttackFactor * Buff.BuffRecord;

            interaction.BeforeHit();
            attack.RunDamage(interaction.TargetChess);
            interaction.AfterHit();

            _triggerState = false;
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"造成{AttackValue}伤害。({Buff.BuffRecord})");
        }
    }
}