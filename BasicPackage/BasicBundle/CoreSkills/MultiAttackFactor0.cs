using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.Resource;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class MultiAttackFactor0 : CoreSkillTemplate
    {
        [R(256)] public float F256 { get; set; }

        protected override string HDescription =>
            $"若前置招式为阴，攻击系数乘{1+F256:P0}。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            SkillContainer skill = BattleScope.Context.CurrentSkill;
            if ((skill.EquipCode >> skill.EquipLayer - 1 & 1) == 0)
                AttackRecord.AttackFactor *= 1 + F256;
        }
    }
}