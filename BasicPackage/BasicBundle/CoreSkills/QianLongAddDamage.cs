using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class QianLongAddDamage : CoreSkillTemplate
    {
        [field: S(ID = 256)] public float F256 { get; set; }

        protected override string HDescription =>
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