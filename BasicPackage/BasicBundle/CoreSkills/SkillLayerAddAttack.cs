using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class SkillLayerAddAttack : CoreSkillTemplate
    {
        [R(256)] public float F256 { get; set; }

        protected override string HDescription =>
            $"增加此招式技能层数*{F256:P0}的攻击系数。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            if (BattleScope.Context.CurrentSkill is CoreSkillContainer core)
                AttackRecord.AttackFactor += F256 * core.EquipLayer;
        }
    }
}