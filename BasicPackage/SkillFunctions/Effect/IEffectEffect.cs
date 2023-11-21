using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;

namespace InfinityWorldChess.SkillFunctions
{
    public interface IEffectEffect : IEquippable<BattleRole>, IOverlayable<BattleRole>, IBuffAttached
    {
    }
}