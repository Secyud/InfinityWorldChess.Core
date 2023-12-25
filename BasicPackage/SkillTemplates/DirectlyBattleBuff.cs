using System.Runtime.InteropServices;
using InfinityWorldChess.BattleBuffDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillTemplates
{
    [Guid("BA656172-D451-7E15-9FF6-F04B8A3C3506")]
    public class DirectlyBattleBuff:BattleUnitBuff,IObjectAccessor<BattleUnitBuff>
    {
        public BattleUnitBuff Value => this;
    }
}