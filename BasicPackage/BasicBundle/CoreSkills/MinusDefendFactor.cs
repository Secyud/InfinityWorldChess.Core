using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class MinusDefendFactor : CoreSkillTemplate
    {
        [field: S(ID = 256)] public float F256 { get; set; }

        protected override string HDescription =>
            $"此招式无视敌方{F256:P0}防御。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            AttackRecord.DefendFactor -= F256;
        }
    }
}