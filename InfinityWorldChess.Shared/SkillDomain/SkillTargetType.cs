using System;

namespace InfinityWorldChess.SkillDomain
{
	[Flags]
	public enum SkillTargetType : byte
	{
		None = 0x00,
		Self = 0x01,
		Enemy = 0x02,
		Teammate = 0x04,
		SelfAndTeammate = Self|Teammate,
		ExcludeSelf = Enemy|Teammate,
		SelfAndEnemy = Self|Enemy,
		All = Self|Enemy|Teammate
	}
}