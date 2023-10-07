using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    /// <summary>
    /// recorder buff, it will remove from character while recorder trig.
    /// unless recorder is null.
    /// </summary>
    public class RecorderBuff : SkillBuffBase
    {
        [field: S] public IBuffRecorder BuffRecorder { get; set; }
        public override string ShowDescription => base.ShowDescription +
                                         BuffRecorder?.ShowDescription;

        public override void Install(BattleRole target)
        {
            base.Install(target);
            BuffRecorder?.Install(target, this);
        }

        public override void UnInstall(BattleRole target)
        {
            base.UnInstall(target);
            BuffRecorder?.UnInstall(target, this);
        }

        public override void Overlay(IBuff<BattleRole> finishBuff)
        {
            base.Overlay(finishBuff);
            if (finishBuff is not RecorderBuff buff)
            {
                return;
            }
            BuffRecorder?.Overlay(buff.BuffRecorder, this);
        }
    }
}