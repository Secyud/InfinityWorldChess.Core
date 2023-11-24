using InfinityWorldChess.BattleBuffDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillTemplates
{
    public class DirectlyBattleBuff:BattleRoleBuff,IObjectAccessor<BattleRoleBuff>
    {
        public BattleRoleBuff Value => this;
    }
}