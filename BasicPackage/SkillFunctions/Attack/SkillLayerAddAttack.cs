using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillFunctions.Attack
{
    public class SkillLayerAddAttack : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        public override string Description => base.Description +
                                                  $"增加此招式技能层数*{F256:P0}的攻击系数。";

        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);

            CoreSkillContainer skill =
                BattleScope.Instance.Get<CoreSkillActionService>().CoreSkill;

            if (skill is not null)
            {
                AttackRecord.AttackFactor += F256 * skill.EquipLayer;
            }
        }
    }
}