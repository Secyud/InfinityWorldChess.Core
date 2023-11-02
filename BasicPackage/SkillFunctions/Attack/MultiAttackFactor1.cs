using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillFunctions.Attack
{
    public class MultiAttackFactor1 : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        public override string Description => base.Description +
                                                  $"若前置招式为阳，攻击系数乘{1 + F256:P0}。";

        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);

            CoreSkillContainer skill =
                BattleScope.Instance.Get<CoreSkillActionService>().CoreSkill;
            if (skill is not null &&
                (skill.EquipCode >> skill.EquipLayer - 1 & 1) > 0)
            {
                AttackRecord.AttackFactor *= 1 + F256;
            }
        }
    }
}