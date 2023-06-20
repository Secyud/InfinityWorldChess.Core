using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.Resource;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class CoreSkillTemplate : SkillWithFlowBase, ICoreSkill
    {
        [R(13)] public float AttackFactor { get; set; }
        [R(14)] public float FixedAttackValue { get; set; }
        [R(15)] public byte FullCode { get; set; }
        [R(16)] public byte MaxLayer { get; set; }
        [R(17)] public byte ConditionCode { get; set; }
        [R(18)] public byte ConditionMask { get; set; }
        protected override string HDescription => "没有特殊效果。";

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddCoreSkillInfo(this);
        }

        protected AttackRecordBuff AttackRecord;

        protected override string HideDescription =>
            $"{base.HideDescription}\r\n · 攻击系数:{AttackFactor}\r\n · 攻击基值:{FixedAttackValue}";

        public override void Release()
        {
            base.Release();
            AttackRecord = null;
        }

        protected override void OnInteraction(SkillInteraction interaction)
        {
            if (interaction.TargetChess is ICanDefend defend)
            {
                float damage = AttackRecord.RunDamage(defend);
                HexCell cell = interaction.TargetChess.Unit.Location;
                if (defend.HealthValue < 0)
                    BattleScope.Context.RemoveBattleChess(interaction.TargetChess);

                BattleScope.Context.CreateText(cell, (int)damage, Color.red);
            }
        }

        protected override void PreInteraction(SkillInteraction interaction)
        {
            AttackRecord = interaction.SetAttack();
            AttackRecord.AttackFactor = AttackFactor;
            AttackRecord.AttackFixedValue = FixedAttackValue;
            AttackRecord.TargetCount = Targets.Value.Length;
            if (this.FitWeapon(interaction.LaunchChess.Belong))
                AttackRecord.DamageFactor += 0.5f;
        }
    }
}