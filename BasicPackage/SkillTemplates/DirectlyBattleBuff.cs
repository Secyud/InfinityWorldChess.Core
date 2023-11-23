using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillTemplates
{
    public class DirectlyBattleBuff:SkillBuff,IObjectAccessor<SkillBuff>
    {
        public SkillBuff Value => this;
    }
}