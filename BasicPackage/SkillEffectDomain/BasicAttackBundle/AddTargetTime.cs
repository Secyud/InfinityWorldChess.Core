using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    /// <summary>
    /// 延缓敌方行动时间
    /// </summary>
    public class AddTargetTime : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        public override string Description=>base.Description+
                                                $"此招式延缓敌方{F256:N0}时序。";
        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            BattleRole chess = interaction.TargetChess;
            chess.Time += chess.GetTimeAdd() * F256;
        }
    }
}