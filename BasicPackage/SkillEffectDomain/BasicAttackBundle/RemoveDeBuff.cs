using System.Linq;
using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class RemoveDeBuff : BasicAttack
    {
        public override string ShowDescription=>base.ShowDescription+
                                                "按照顺序移除自身第一个负面效果。";
        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);

            IBuff<BattleRole> buff = interaction.LaunchChess.Values
                .FirstOrDefault(u => u is IDeBuff);
            if (buff is not null)
            {
                interaction.LaunchChess.UnInstall(buff.GetType());
                interaction.TargetChess?.Install(buff);
            }
        }
    }
}