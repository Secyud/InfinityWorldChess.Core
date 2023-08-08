using System.Linq;
using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class RemoveDeBuff : CoreSkillTemplate
    {
        protected override string HDescription =>
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