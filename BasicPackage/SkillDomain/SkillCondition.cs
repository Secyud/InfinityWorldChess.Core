#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class SkillCondition
	{
		public static bool IsFist(BattleRole chess)
		{
			return chess.Role.Equipment[BodyType.Kiling] is null ||
				((chess.Role.Equipment[BodyType.Kiling].TypeCode ^ 0b00000000) & 0b00001111) == 0;
		}
	}
}