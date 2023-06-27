using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class AddMaim : DeBuffCoreBase
    {
        [field: S(ID = 256)] public int D256 { get; set; }

        [field: S(ID = 257)] public float F257 { get; set; }

        protected override string HDescription =>
            $"为敌方添加致残({D256}, {F257:N0})。";

        protected override IBuff<RoleBattleChess> ConstructBuff()
        {
            return new Maim
            {
                Value = D256,
                TimeRecorder = {TimeFinished = F257}
            };
        }
    }
}