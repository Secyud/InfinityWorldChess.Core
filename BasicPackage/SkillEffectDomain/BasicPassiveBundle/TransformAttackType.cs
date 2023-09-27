using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.AttackDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicPassiveBundle
{
    public class TransformAttackType : BattlePassive, IActionable<SkillInteraction>
    {
        [field: S] public int AttackType { get; set; }

        public int Priority => 1;

        public override string ShowDescription =>
            $"将所有攻击转化为{AttackType switch { 0 => "外伤", 1 => "内伤", 2 => "精神", _ => "未知" }}属性。";

        public override void OnBattleInitialize(BattleRole chess)
        {
            BattleEventsBuff e = chess.GetBattleEvents();
            e.PrepareLaunch.Add(this);
        }

        public void Active(SkillInteraction target)
        {
            AttackRecordBuff attack = target.TypeBuff.Get<AttackRecordBuff>();
            if (attack is not null)
                attack.AttackType = (AttackType)AttackType;
        }
    }
}