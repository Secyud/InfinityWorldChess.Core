using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class LowHealthAddAttack : CoreSkillTemplate
    {
        [field: S(ID = 256)] public float F256 { get; set; }

        protected override string HDescription =>
            $"每损失1%的生命值增加{F256:P0}的攻击系数。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            RoleBattleChess role = interaction.LaunchChess.Belong;
            float value = 1 - role.HealthValue / role.MaxHealthValue;
            AttackRecord.AttackFactor += value * F256 * 100;
        }
    }
}