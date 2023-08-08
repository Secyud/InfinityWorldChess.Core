using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    /// <summary>
    /// 吸血
    /// </summary>
    public class DamageAddHealth : CoreSkillTemplate
    {
        [field: S(ID = 256)] private float F256 { get; set; }

        protected override string HDescription =>
            $"此招式将造成伤害的{F256:P0}转化为自身生命。";
			
        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            BattleRole battleRole = interaction.LaunchChess;
            battleRole.HealthValue += AttackRecord.TotalDamage * F256;
        }
    }
}