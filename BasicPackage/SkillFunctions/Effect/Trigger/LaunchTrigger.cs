using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;

namespace InfinityWorldChess.SkillFunctions.Effect
{
    public class LaunchTrigger : BuffTriggerBase, IActionable<SkillInteraction>
    {
        private BattleEvents _record;
        public override string Description => "每次释放技能触发," + base.Description;
        public int Priority => 65535;

        public override void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            base.Install(target, buff);
            _record = target.GetProperty<BattleEvents>();
            _record.LaunchCallback.Add(this);
        }

        public override void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            base.UnInstall(target, buff);
            _record.LaunchCallback.Remove(this);
        }

        public void Active(SkillInteraction target)
        {
            foreach (ITriggerEffect e in Effects)
            {
                e.Active();
            }
        }
    }
}