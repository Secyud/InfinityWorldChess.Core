using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class FlashEffect : IActiveSkillEffect
    {
        public void Cast(BattleRole role, BattleCell releasePosition, ISkillRange range, IActiveSkill skill)
        {
            HexDirection direction = role.Unit.Location.DirectionTo(releasePosition);
            role.Unit.Location = releasePosition;
            role.Direction = direction;
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("释放效果：闪现至目标点");
        }
    }
}