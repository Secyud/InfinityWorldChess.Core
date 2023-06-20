using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public abstract class DeBuffCoreBase : CoreSkillTemplate
    {
        protected abstract IBuff<RoleBattleChess> ConstructBuff();

        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            if (interaction.TargetChess is RoleBattleChess chess)
                chess.Install(ConstructBuff());
        }
    }
}