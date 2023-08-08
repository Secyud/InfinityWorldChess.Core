using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class CoreSkillTemplate : SkillWithFlowBase, ICoreSkill
    {
        [field: S(ID = 9)] public float AttackFactor { get; set; }
        [field: S(ID = 10)] public float FixedAttackValue { get; set; }

        [field: S(ID = 11)] public byte FullCode { get; set; }
        [field: S(ID = 12)] public byte MaxLayer { get; set; }
        [field: S(ID = 13)] public byte ConditionCode { get; set; }
        [field: S(ID = 14)] public byte ConditionMask { get; set; }
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
                    interaction.TargetChess.Unit.Die();

                BattleScope.Instance.CreateNumberText(cell, (int)damage, Color.red);
            }
        }

        protected override void PreInteraction(SkillInteraction interaction)
        {
            AttackRecord = interaction.SetAttack();
            AttackRecord.AttackFactor = AttackFactor;
            AttackRecord.AttackFixedValue = FixedAttackValue;
            AttackRecord.TargetCount = Targets.Value.Length;
            if (this.FitWeapon(interaction.LaunchChess))
                AttackRecord.DamageFactor += 0.5f;
        }
    }
}