using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillEffectDomain.Abstractions;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class AddMaim : DeBuffCoreBase
    {
        [field: S] public int D256 { get; set; }

        [field: S] public float F257 { get; set; }

        public override string ShowDescription=>base.ShowDescription+
                                                $"为敌方添加致残({D256}, {F257:N0})。";

        protected override IBuff<BattleRole> ConstructBuff()
        {
            return new Maim
            {
                Value = D256,
                TimeRecorder = {TimeFinished = F257}
            };
        }
    }
}