using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class AddEnergy : BasicAttack
    {
        [field: S] public float RecoverEnergyValue { get; set; }
        [field: S] public byte BodyType { get; set; }
        public override string ShowDescription=>
            base.ShowDescription + 
            $"{p}此招式恢复自身{RecoverEnergyValue:P0}点内力。受技能【生】属性影响。";

        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            interaction.LaunchChess.EnergyValue += RecoverEnergyValue * Skill.Living;
        }
    }
}