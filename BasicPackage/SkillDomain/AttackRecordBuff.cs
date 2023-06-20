#region

using InfinityWorldChess.BattleDomain;
using System;

#endregion

namespace InfinityWorldChess.SkillDomain
{
    public sealed class AttackRecordBuff : SkillInteractionBuffBase
    {
        private float _defend;

        private float _penetration = 10;

        public float Attack { get; set; }
        public AttackType AttackType { get; set; }

        public float Penetration
        {
            get => _penetration;
            set => _penetration = Math.Max(value, 1);
        }

        public float AttackFixedValue { get; set; }

        public float AttackFactor { get; set; } = 1f;

        public float Defend
        {
            get => _defend;
            set => _defend = Math.Max(0, value);
        }

        public float DamageFactor { get; set; } = 1f;

        public float DefendFactor { get; set; } = 0f;

        public int TargetCount { get; set; }
        
        public float TotalDamage { get; private set; }

        public float RunDamage(ICanDefend target)
        {
            float factor = 0.1f + AttackFactor / TargetCount;
            float basic = Attack * factor + AttackFixedValue;
            float afterDefend = basic * _penetration / (_penetration + Defend);
            float damage = afterDefend * DamageFactor * (1 - DefendFactor);
            target.HealthValue -= damage;
            float realDamage = damage + Math.Min(target.HealthValue, 0);
            TotalDamage += realDamage;
            return realDamage;
        }
    }
}