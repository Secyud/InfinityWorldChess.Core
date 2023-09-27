using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.Ugf;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public class LaunchTrigger : BuffTriggerBase, IActionable<SkillInteraction>
    {
        private BattleEventsBuff _record;
        public override string ShowDescription => "每次释放技能触发," + Effect.ShowDescription;
        public int Priority => 65535;

        public override void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            base.Install(target, buff);
            _record = target.GetBattleEvents();
            _record.LaunchCallback.Add(this);
        }

        public override void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            base.UnInstall(target, buff);
            _record.LaunchCallback.Remove(this);
        }

        public void Active(SkillInteraction target)
        {
            Effect.Active();
        }
    }
}