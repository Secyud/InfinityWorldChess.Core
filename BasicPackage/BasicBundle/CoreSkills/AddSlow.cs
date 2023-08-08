using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.BasicBundle.CoreSkills.Abstractions;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class AddSlow : DeBuffCoreBase
    {
       [field: S(ID = 256)] public int D256 { get; set; }

       [field: S(ID = 257)] public float F257 { get; set; }

        protected override string HDescription =>
            $"为敌方添加缓速({D256}, {F257:N0})。";

        protected override IBuff<BattleRole> ConstructBuff()
        {
            return new Slow
            {
                Value = D256,
                TimeRecorder = {TimeFinished = F257}
            };
        }
    }
}