using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.BasicBundle.CoreSkills.Abstractions
{
    public abstract class DeBuffCoreBase : CoreSkillTemplate
    {
        protected abstract IBuff<BattleRole> ConstructBuff();

        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            if (interaction.TargetChess is BattleRole chess)
                chess.Install(ConstructBuff());
        }
    }
}