using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.BasicBundle.BattleBuffs.Recorders;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicPassiveBundle
{
    public class RemoveDeBuff :BattlePassive
    {
        [field:S] private byte DeBuffType { get; set; }

        [field:S] private int Value { get; set; }

        private BattleRole _chess;

        public override string ShowDescription =>
            $"每回合移除{DeBuffType switch { 1 => "灼烧", 2 => "冰冻", 3 => "中毒", _ => "未知" }}状态({Value})。";
        
        public override void OnBattleInitialize(BattleRole chess)
        {
            BattleScope.Instance.Battle.RoundBeginAction += Active;
        }

        private void Active()
        {
            switch (DeBuffType)
            {
                case 3:
                {
                    TimeRecorder recorder = _chess.Get<PoisonBuff>()?.TimeRecorder;
                    if (recorder is null) return;
                    recorder.TimeFinished -= Value;
                    if (recorder.TimeFinished <= 0)
                        _chess.UnInstall<PoisonBuff>();
                    return;
                }
                case 2:
                {
                    FrozenBuff recorder = _chess.Get<FrozenBuff>();
                    recorder.LayerCount -= Value;
                    if (recorder.LayerCount <= 0)
                        _chess.UnInstall<FrozenBuff>();
                    return;
                }
                case 1:
                {
                    FiringBuff recorder = _chess.Get<FiringBuff>();
                    recorder.LayerCount -= Value;
                    if (recorder.LayerCount <= 0)
                        _chess.UnInstall<FiringBuff>();
                    return;
                }
                default:
                    return;
            }
        }
    }
}