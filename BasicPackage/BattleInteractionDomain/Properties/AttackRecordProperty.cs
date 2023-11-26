﻿#region

using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;

#endregion

namespace InfinityWorldChess.BattleInteractionDomain
{
    /// <summary>
    /// all attack skill should build attack record buff to run standard attack procedure.
    /// </summary>
    public sealed class AttackRecordProperty : BattleInteractionPropertyBase
    {
        public float Attack { get; set; }
        public AttackType AttackType { get; set; }
        public float Penetration { get; set; }
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

        public bool IsAttacked { get; private set; } = false;

        public float RunDamage(ICanDefend target)
        {
            float penetration = I(Penetration);
            float factor = 0.1f + O(AttackFactor) / I(TargetCount);
            float basic = O(Attack) * factor + O(AttackFixedValue);
            float afterDefend = basic * penetration / (penetration + O(Defend));
            float damage = afterDefend * DamageFactor * O(1 - O(DefendFactor));
            target.HealthValue -= damage;
            float realDamage = damage + Math.Min(target.HealthValue, 0);
            TotalDamage += realDamage;
            IsAttacked = true;
            return realDamage;
        }
    }
}