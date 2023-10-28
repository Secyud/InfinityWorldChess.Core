using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    /// <summary>
    /// some effect will not run if them doesn't fit some expression.
    /// use actionable limit effect and select effect limit.
    /// </summary>
    public class ActionableLimitEffect : IActionableEffect
    {
        [field: S] public IActionableEffect Effect { get; set; }
        [field: S] public IEffectLimit Limit { get; set; }
        public string Description => Effect.Description;
        public int Priority => Effect.Priority;

        public void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            Effect.Install(target, buff);
        }

        public void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            Effect.UnInstall(target, buff);
        }

        public void Overlay(IBuffEffect sameEffect, IBuff<BattleRole> buff)
        {
            if (sameEffect is ActionableLimitEffect effect)
            {
                Effect.Overlay(effect.Effect, buff);
            }
        }

        public void Active(SkillInteraction target)
        {
            if (Limit.CheckLimit(target))
                Effect.Active(target);
        }

        public void SetSkill(IActiveSkill skill)
        {
            Effect.SetSkill(skill);
        }
    }
}