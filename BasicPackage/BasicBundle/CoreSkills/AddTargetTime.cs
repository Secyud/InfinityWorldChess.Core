using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    /// <summary>
    /// 延缓敌方行动时间
    /// </summary>
    public class AddTargetTime : CoreSkillTemplate
    {
        [R(256)] public float F256 { get; set; }

        protected override string HDescription =>
            $"此招式延缓敌方{F256:N0}时序。";
        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            RoleBattleChess chess = interaction.TargetChess.Belong;
            chess.Time += chess.GetTimeAdd() * F256;
        }
    }
}