using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexUtilities;

namespace InfinityWorldChess.SkillEffectDomain.BasicMoveBundle
{
	public class FlashEffect : IActiveSkillEffect
	{
		public string ShowDescription => "闪现至目标点";
		public void Cast( BattleRole role, HexCell releasePosition, ISkillRange range,IActiveSkill skill)
		{
			HexDirection direction = releasePosition.DirectionTo(role.Unit.Location);
			role.Unit.Location = releasePosition;
			role.Direction = direction;
		}
	}
}