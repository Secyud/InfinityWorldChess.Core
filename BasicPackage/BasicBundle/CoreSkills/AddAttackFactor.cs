using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class AddAttackFactor : CoreSkillTemplate
    {
        [field: S(ID = 256)] public float F256 { get; set; }

        protected override string HDescription =>
            $"此招式攻击增加{F256:P0}。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            AttackRecord.AttackFactor += F256;
        }
    }
}