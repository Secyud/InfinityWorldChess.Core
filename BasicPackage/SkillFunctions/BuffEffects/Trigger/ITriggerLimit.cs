using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// some effect will not run if them doesn't fit some expression.
    /// use actionable limit effect and select effect limit.
    /// </summary>
    public interface ITriggerLimit
    {
        bool CheckLimit(SkillInteraction target);
    }
}