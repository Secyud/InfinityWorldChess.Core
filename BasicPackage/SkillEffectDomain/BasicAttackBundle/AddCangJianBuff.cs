using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class AddCangJianBuff : BasicAttack
    {
       [field: S] public float CangJianValue { get; set; }
       [field: S] public float VulnerValue { get; set; }
       [field: S] public float VulnerRound { get; set; }

        public override string ShowDescription =>base.ShowDescription+
            $"{p}为自己添加藏剑状态，本回合招式增伤{CangJianValue:P0}。{p}为自己添加易伤({VulnerValue:P0}, {VulnerRound:N0})。";
			
        protected override void PostSkill(BattleRole battleChess, HexCell releasePosition)
        {
            base.PostSkill(battleChess, releasePosition);
            battleChess.Install(
                new CangJianBuff
                {
                    Factor = CangJianValue,
                    TurnRecorder = {TurnFinished = 1}
                }
            );
            battleChess.Install(
                new VulnerBuff.TimeRemove
                {
                    Factor = VulnerValue,
                    TimeRecorder = {TimeFinished = VulnerRound}
                }
            );
        }
    }
}