﻿using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillFunctions.Attack
{
    public class AddEnergy : BasicAttack
    {
        [field: S] public float RecoverFactor { get; set; }
        [field: S] public float RecoverValue { get; set; }

        public override string Description =>
            base.Description +
            $"{p}此招式恢复自身内力，受技能[生]属性影响。";

        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);

            interaction.LaunchChess.EnergyValue +=
                RecoverValue + RecoverFactor * Skill.Living;
        }
    }
}