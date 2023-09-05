using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class AddAttackFactor : BasicAttack
    {
        [field: S] public float AddedAttackFactor { get; set; }

        public override string ShowDescription => base.ShowDescription +
                                                  $"{p}此招式攻击增加{AddedAttackFactor:P0}。";

        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            AttackRecord.AttackFactor += AddedAttackFactor;
        }
    }
}