using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleSkillDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class SkillLayerAddAttack : CoreSkillTemplate
    {
        [field: S(ID = 256)] public float F256 { get; set; }

        protected override string HDescription =>
            $"增加此招式技能层数*{F256:P0}的攻击系数。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            if (BattleScope.Instance.Get<SkillRefreshService>().Skill
                is CoreSkillContainer core)
                AttackRecord.AttackFactor += F256 * core.EquipLayer;
        }
    }
}