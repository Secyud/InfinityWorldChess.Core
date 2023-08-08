using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
    public class CangJianBuff : DamageBuff.WithTurnRecord
    {
        public override string ShowName => "藏剑";

        public override void Overlay(IBuff<BattleRole> finishBuff)
        {
        }
    }
}