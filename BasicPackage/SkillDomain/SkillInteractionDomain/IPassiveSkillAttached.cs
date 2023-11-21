namespace InfinityWorldChess.SkillDomain
{
    public interface IPassiveSkillAttached:IPassiveSkillEffect
    {
        PassiveSkill Skill { get; set; }
    }
}