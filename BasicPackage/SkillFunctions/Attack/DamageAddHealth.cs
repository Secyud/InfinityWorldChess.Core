using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillFunctions.Attack
{
    /// <summary>
    /// 吸血
    /// </summary>
    public class DamageAddHealth : BasicAttack
    {
        [field: S] private float F256 { get; set; }

        public override string Description=>base.Description+
                                                $"此招式将造成伤害的{F256:P0}转化为自身生命。";
			
        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            BattleRole battleRole = interaction.LaunchChess;
            battleRole.HealthValue += AttackRecord.TotalDamage * F256;
        }
    }
}