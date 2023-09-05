using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.SkillEffectDomain.BasicAttackBundle;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.ZhouYiBundle
{
    public class QianLongAddDamage : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        public override string ShowDescription=>base.ShowDescription+
                                                $"潜龙状态将会额外增加此招式{F256:P0}伤害。";
        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);
            QianLongBuff buff = interaction.LaunchChess.Get<QianLongBuff>();
            if (buff is not null)
                AttackRecord.DamageFactor += F256 * buff.TrigRecorder.TrigFinished;
        }
    }
}