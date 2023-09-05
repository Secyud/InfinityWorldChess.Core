using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleSkillDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class MultiAttackFactor0 : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        public override string ShowDescription=>base.ShowDescription+
                                                $"若前置招式为阴，攻击系数乘{1+F256:P0}。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            SkillContainer skill = BattleScope.Instance.Get<SkillObservedService>().Skill;
            if ((skill.EquipCode >> skill.EquipLayer - 1 & 1) == 0)
                AttackRecord.AttackFactor *= 1 + F256;
        }
    }
}