using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public interface IBuffFunction
    {
        void Install(BattleRole target, IBuff<BattleRole> buff);
        void UnInstall(BattleRole target, IBuff<BattleRole> buff);
    }
}