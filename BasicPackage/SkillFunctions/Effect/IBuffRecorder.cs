using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillFunctions.Effect
{
    /// <summary>
    /// buff with uninstall while recorder trig.
    /// </summary>
    public interface  IBuffRecorder:IHasDescription,IBuffFunction
    {
        void Overlay(IBuffRecorder sameRecorder, IBuff<BattleRole> buff);
    }
}