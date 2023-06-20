using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.Resource;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    /// <summary>
    /// 吸血
    /// </summary>
    public class DamageAddHealth : CoreSkillTemplate
    {
        [R(256)] private float F256 { get; set; }

        protected override string HDescription =>
            $"此招式将造成伤害的{F256:P0}转化为自身生命。";
			
        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            RoleBattleChess role = interaction.LaunchChess.Belong;
            role.HealthValue += AttackRecord.TotalDamage * F256;
        }
    }
}