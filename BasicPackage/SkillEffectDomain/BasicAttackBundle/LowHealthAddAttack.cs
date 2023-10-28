using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class LowHealthAddAttack : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        public override string Description=>base.Description+
                                                $"每损失1%的生命值增加{F256:P0}的攻击系数。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            BattleRole battleRole = interaction.LaunchChess;
            float value = 1 - battleRole.HealthValue / battleRole.MaxHealthValue;
            AttackRecord.AttackFactor += value * F256 * 100;
        }
    }
}