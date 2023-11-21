using InfinityWorldChess.SkillFunctions;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillTemplates
{
    public class DirectlyBattleBuff:SkillBuff,IObjectAccessor<SkillBuff>
    {
        public SkillBuff Value => this;
    }
}