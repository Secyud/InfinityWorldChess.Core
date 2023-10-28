using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.AttackDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    public class NormalAttackEffect : AttackEffectBase
    {
        [field: S] public float AttackValue { get; set; }
        [field: S] public float AttackFactor { get; set; }
        public int LayerCount { get; set; }

        public override string Description =>
            $"触发{AttackValue}伤害。({LayerCount})";

        public override void Overlay(IBuffEffect sameEffect, IBuff<BattleRole> buff)
        {
            if (sameEffect is not NormalAttackEffect effect)
                return;

            effect.AttackValue = AttackValue;
            effect.LayerCount += LayerCount;
        }

        private BattleEvents _record;

        protected override void SetAttack(SkillInteraction interaction)
        {
            AttackRecordBuff attack = interaction.SetAttack();
            attack.AttackType = AttackType.Magical;
            attack.Attack = AttackValue;
            attack.AttackFactor += AttackFactor * LayerCount;
        }
    }
}