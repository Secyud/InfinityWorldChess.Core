namespace InfinityWorldChess.SkillDomain
{
	public interface IFormSkill : IActiveSkill
	{
		public byte Type { get;set; }

		public byte State { get; set;}
	}
}