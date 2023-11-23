using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public abstract class BattleRecordProperty: IBuff<BattleRole>
    {
        public int Id => -1;
        public byte BuffLevel => 0;
        
        public virtual void Install(BattleRole target)
        {
        }

        public virtual void UnInstall(BattleRole target)
        {
        }


        public virtual void Overlay(IBuff<BattleRole> finishBuff)
        {
            throw new UgfException($"{GetType()} cannot be overlapped!");
        }
    }
}