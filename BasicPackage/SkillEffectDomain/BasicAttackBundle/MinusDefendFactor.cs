using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class MinusDefendFactor : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        public override string ShowDescription=>base.ShowDescription+
                                                $"此招式无视敌方{F256:P0}防御。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            AttackRecord.DefendFactor -= F256;
        }
    }
}