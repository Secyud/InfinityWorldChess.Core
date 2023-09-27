using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public class RecorderBuff : IBuff<BattleRole>, IHasDescription
    {
        [field: S] public int Id { get; set; }
        [field: S] public IBuffRecorder BuffRecorder { get; set; }
        [field: S] public IBuffEffect BuffEffect { get; set; }

        public string ShowDescription => BuffEffect?.ShowDescription +
                                         BuffRecorder?.ShowDescription;

        public void Install(BattleRole target)
        {
            BuffEffect?.Install(target, this);
            BuffRecorder?.Install(target, this);
        }

        public void UnInstall(BattleRole target)
        {
            BuffEffect?.UnInstall(target, this);
            BuffRecorder?.UnInstall(target, this);
        }

        public void Overlay(IBuff<BattleRole> finishBuff)
        {
            if (finishBuff is not RecorderBuff buff)
            {
                return;
            }

            BuffEffect?.Overlay(buff.BuffEffect, this);
            BuffRecorder?.Overlay(buff.BuffRecorder, this);
        }
    }
}