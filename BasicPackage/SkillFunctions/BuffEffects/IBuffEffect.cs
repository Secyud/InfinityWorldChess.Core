using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;

namespace InfinityWorldChess.SkillFunctions
{
    public interface IBuffEffect : IEquippable<BattleRole>, IOverlayable<BattleRole>, IBuffAttached
    {
    }
}