using InfinityWorldChess.SkillDomain.SkillInteractionDomain;

namespace InfinityWorldChess.SkillFunctions.Effect
{
    /// <summary>
    /// some effect will not run if them doesn't fit some expression.
    /// use actionable limit effect and select effect limit.
    /// </summary>
    public interface IEffectLimit
    {
        bool CheckLimit(SkillInteraction target);
    }
}