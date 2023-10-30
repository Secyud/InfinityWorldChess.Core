using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class CenterAddDamage : BasicAttack
    {
        [field: S] public int Factor { get; set; }

        public override string Description=>base.Description+
                                            $"此招式对；落点的攻击增加{Factor:p0}。";

        private BattleCell _cell;

        protected override void PreSkill(BattleRole battleChess, BattleCell releasePosition)
        {
            base.PreSkill(battleChess, releasePosition);
            _cell = releasePosition;
        }
        
        

        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            if (interaction.TargetChess.Unit.Location == _cell)
            {
                AttackRecord.AttackFactor += Factor;
            }
        }
    }
}