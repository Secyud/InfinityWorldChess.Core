#region

using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;

#endregion

namespace InfinityWorldChess.SkillDomain.AttackDomain
{
    /// <summary>
    /// all attack skill should build attack record buff to run standard attack procedure.
    /// </summary>
    public sealed class AttackRecordBuff : SkillInteractionBuffBase
    {
        public override int Id => -1;
        public float Attack { get; set; }
        public AttackType AttackType { get; set; }
        public float Penetration { get; set; } = 10;
        public float AttackFixedValue { get; set; }

        /// <summary>
        /// effect attack value.
        /// </summary>
        public float AttackFactor { get; set; } = 1f;

        public float Defend { get; set; }

        /// <summary>
        /// the final damage will take it.
        /// </summary>
        public float DamageFactor { get; set; } = 1f;

        /// <summary>
        /// the final damage will take (1- it).
        /// defend factor better not lager than 1.
        /// </summary>
        public float DefendFactor { get; set; }

        /// <summary>
        /// the efficient target, it will effect attack.
        /// </summary>
        public int TargetCount { get; set; }

        /// <summary>
        /// the total damage for this attack.
        /// for some multistage skill.
        /// the total damage can be used for some
        /// effect based on it like life stealing.
        /// </summary>
        public float TotalDamage { get; private set; }

        public float RunDamage(ICanDefend target)
        {
            float penetration = Math.Max(Penetration, 1);
            float defend = Math.Max(0, Defend);
            float factor = 0.1f + AttackFactor / TargetCount;
            float basic = Attack * factor + AttackFixedValue;
            float afterDefend = basic * penetration / (penetration + defend);
            float damage = afterDefend * DamageFactor * (1 - DefendFactor);
            target.HealthValue -= damage;
            float realDamage = damage + Math.Min(target.HealthValue, 0);
            TotalDamage += realDamage;
            return realDamage;
        }

    }
}