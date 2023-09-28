using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    public interface IBuffEffect : IHasDescription,IBuffFunction,ISkillBuff
    {
        void Overlay(IBuffEffect sameEffect, IBuff<BattleRole> buff);
    }
}