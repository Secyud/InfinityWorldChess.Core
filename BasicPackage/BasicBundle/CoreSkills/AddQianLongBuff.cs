using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class AddQianLongBuff : CoreSkillTemplate
    {
        [field: S(ID = 256)] public float F256 { get; set; }

        [field: S(ID = 257)] public int D257 { get; set; }

        protected override string HDescription =>
            $"为自己添加潜龙状态，所有天龙掌招式增加{F256:P0}伤害，持续至使用{D257}次天龙掌之后。";

        protected override void PostSkill(BattleRole battleChess, HexCell releasePosition)
        {
            base.PostSkill(battleChess, releasePosition);
            QianLongBuff qianLongBuff = battleChess.GetOrInstall<QianLongBuff>();
            qianLongBuff.TrigRecorder.TrigFinished = D257;
            qianLongBuff.Factor = F256;
        }
    }
}