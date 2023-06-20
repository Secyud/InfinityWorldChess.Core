using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class AddEnergy : CoreSkillTemplate
    {
        [R(256)] public float F256 { get; set; }
			
        protected override string HDescription =>
            $"此招式恢复自身{F256:P0}内力。";

        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            interaction.LaunchChess.Belong.EnergyValue += F256;
        }
    }
}