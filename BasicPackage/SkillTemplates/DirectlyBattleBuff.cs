using InfinityWorldChess.BattleBuffDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillTemplates
{
    public class DirectlyBattleBuff:BattleUnitBuff,IObjectAccessor<BattleUnitBuff>
    {
        public BattleUnitBuff Value => this;
    }
}