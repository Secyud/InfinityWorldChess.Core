using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleCellDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.HexUtilities;

namespace InfinityWorldChess.SkillFunctions.Move
{
    public class FlashEffect : IActiveSkillEffect
    {
        public string Description => "闪现至目标点";

        public void Cast(BattleRole role, BattleCell releasePosition, ISkillRange range, IActiveSkill skill)
        {
            HexDirection direction = releasePosition.DirectionTo(role.Unit.Location);
            role.Unit.Location = releasePosition;
            role.Direction = direction;
        }
    }
}