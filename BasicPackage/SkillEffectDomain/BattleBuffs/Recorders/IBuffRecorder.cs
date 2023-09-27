using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public interface  IBuffRecorder:IHasDescription,IBuffFunction
    {
        void Overlay(IBuffRecorder thisRecorder, IBuff<BattleRole> buff);
    }
}