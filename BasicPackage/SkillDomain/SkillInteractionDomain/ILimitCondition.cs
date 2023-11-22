namespace InfinityWorldChess.SkillDomain
{
    /// <summary>
    /// some effect will not run if them doesn't fit some expression.
    /// use actionable limit effect and select effect limit.
    /// </summary>
    public interface ILimitCondition
    {
        bool CheckLimit(object sender);
    }
}