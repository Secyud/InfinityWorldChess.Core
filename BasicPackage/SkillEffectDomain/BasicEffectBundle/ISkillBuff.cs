using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    public interface ISkillBuff
    {
        void SetSkill(IActiveSkill skill);
    }
}