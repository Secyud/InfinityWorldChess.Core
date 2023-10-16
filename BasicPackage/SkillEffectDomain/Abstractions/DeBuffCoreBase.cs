using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.SkillEffectDomain.BasicAttackBundle;

namespace InfinityWorldChess.SkillEffectDomain.Abstractions
{
    public abstract class DeBuffCoreBase : BasicAttack
    {
        protected abstract IBuff<BattleRole> ConstructBuff();

        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            if (interaction.TargetChess is BattleRole chess)
            {
                chess.Buff.Install(ConstructBuff());
            }
        }
    }
}