﻿using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillFunctions.Attack
{
    public class MinusDefendFactor : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        public override string Description=>base.Description+
                                                $"此招式无视敌方{F256:P0}防御。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            AttackRecord.DefendFactor -= F256;
        }
    }
}