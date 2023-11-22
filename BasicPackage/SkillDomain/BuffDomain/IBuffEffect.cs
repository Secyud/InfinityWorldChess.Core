using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;

namespace InfinityWorldChess.SkillDomain
{
    public interface IBuffEffect : IEquippable<BattleRole>, 
        IOverlayable<BattleRole>, IBuffAttached
    {
    }
}