using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.SkillFunctions.Effect
{
    public interface ISkillBuff
    {
        void SetSkill(IActiveSkill skill);
    }
}