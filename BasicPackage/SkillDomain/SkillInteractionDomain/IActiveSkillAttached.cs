namespace InfinityWorldChess.SkillDomain
{
    public interface IActiveSkillAttached
    {
        ActiveSkillBase BelongSkill { get; set; }
    }
}