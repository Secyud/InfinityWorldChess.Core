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

        public override string ShowDescription =>
            $"触发{AttackValue}伤害。({LayerCount})";

        public override void Overlay(IBuffEffect thisEffect, IBuff<BattleRole> buff)
        {
            if (thisEffect is not NormalAttackEffect effect)
                return;

            int layerCount = LayerCount + effect.LayerCount - 1;
            effect.AttackValue = AttackValue;
            effect.LayerCount = layerCount;
        }

        private BattleEventsBuff _record;

        protected override void SetAttack(SkillInteraction interaction)
        {
            AttackRecordBuff attack = interaction.SetAttack();
            attack.AttackType = AttackType.Magical;
            attack.Attack = AttackValue;
            attack.AttackFactor += AttackFactor * LayerCount;
        }
    }
}