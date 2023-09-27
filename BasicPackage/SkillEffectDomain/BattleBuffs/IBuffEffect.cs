using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public interface IBuffEffect : IHasDescription,IBuffFunction
    {
        void Overlay(IBuffEffect thisEffect, IBuff<BattleRole> buff);
    }
}