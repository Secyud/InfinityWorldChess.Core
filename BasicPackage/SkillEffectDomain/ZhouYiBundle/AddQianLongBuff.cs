using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillEffectDomain.BasicAttackBundle;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillEffectDomain.ZhouYiBundle
{
    public class AddQianLongBuff : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        [field: S] public int D257 { get; set; }

        public override string ShowDescription=>base.ShowDescription+
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