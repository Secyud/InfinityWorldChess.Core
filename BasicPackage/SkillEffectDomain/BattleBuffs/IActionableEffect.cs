using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public interface IActionableEffect:IBuffEffect
    {
        void Active();

    }
}