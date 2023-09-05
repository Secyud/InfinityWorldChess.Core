using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleSkillDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class SkillLayerAddAttack : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        public override string ShowDescription=>base.ShowDescription+
                                                $"增加此招式技能层数*{F256:P0}的攻击系数。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            if (BattleScope.Instance.Get<SkillObservedService>().Skill
                is CoreSkillContainer core)
                AttackRecord.AttackFactor += F256 * core.EquipLayer;
        }
    }
}