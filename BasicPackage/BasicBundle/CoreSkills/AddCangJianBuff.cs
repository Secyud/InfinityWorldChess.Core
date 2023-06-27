using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class AddCangJianBuff : CoreSkillTemplate
    {
       [field: S(ID = 256)] public float F256 { get; set; }
       [field: S(ID = 257)] public float F257 { get; set; }
       [field: S(ID = 258)] public float F258 { get; set; }

        protected override string HDescription =>
            $"为自己添加藏剑状态，本回合招式增伤{F256:P0}。并为自己添加易伤({F257:P0}, {F258:N0})。";
			
        protected override void PostSkill(IBattleChess battleChess, HexCell releasePosition)
        {
            base.PostSkill(battleChess, releasePosition);
            battleChess.Belong.Install(
                new CangJianBuff
                {
                    Factor = F256,
                    TurnRecorder = {TurnFinished = 1}
                }
            );
            battleChess.Belong.Install(
                new VulnerBuff.TimeRemove
                {
                    Factor = F257,
                    TimeRecorder = {TimeFinished = F258}
                }
            );
        }
    }
}