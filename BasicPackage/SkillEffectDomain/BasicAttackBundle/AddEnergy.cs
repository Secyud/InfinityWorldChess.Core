using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class AddEnergy : BasicAttack
    {
        [field: S] public float RecoverEnergyValue { get; set; }
        public override string ShowDescription=>base.ShowDescription+
                                                $"{p}此招式恢复自身{RecoverEnergyValue:P0}内力。";

        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            interaction.LaunchChess.EnergyValue += RecoverEnergyValue;
        }
    }
}