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

            IBuff<RoleBattleChess> buff = interaction.LaunchChess.Belong.Buffs
                .FirstOrDefault(u => u.Value is IDeBuff).Value;
            if (buff is not null)
            {
                interaction.LaunchChess.Belong.UnInstall(buff.GetType());
                interaction.TargetChess.Belong?.Install(buff);
            }
        }
    }
}