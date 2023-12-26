namespace InfinityWorldChess.SkillDomain
{
    public interface IFormSkill : IActiveSkill
    {
        public byte Type { get; }

        public byte State { get; }
    }
}