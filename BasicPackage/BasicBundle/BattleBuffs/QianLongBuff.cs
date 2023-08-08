using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleSkillDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
    public class QianLongBuff : DamageBuff.WithTrigRecorder
    {
        public override string ShowName => "潜龙";

        public override string ShowDescription => "潜龙状态下，名称含有‘龙’字的招式" + base.ShowDescription;

        public override void Active(SkillInteraction target)
        {
            if (BattleScope.Instance.Get<SkillRefreshService>().Skill.Skill is IShowable shown &&
                shown.ShowName.Contains('龙'))
                base.Active(target);
        }
    }
}