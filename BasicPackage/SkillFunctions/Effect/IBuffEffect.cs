using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillFunctions.Effect
{
    public interface IBuffEffect : IHasDescription,IBuffFunction,ISkillBuff
    {
        void Overlay(IBuffEffect sameEffect, IBuff<BattleRole> buff);
    }
}