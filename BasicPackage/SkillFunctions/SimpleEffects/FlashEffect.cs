using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class FlashEffect : ISkillActionEffect
    {
        public ActiveSkillBase BelongSkill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("闪现至目标点。");
        }

        public void Invoke(BattleRole battleChess, BattleCell releasePosition)
        {
            if (!releasePosition.Unit)
            {
                HexDirection direction = BelongSkill.Role.Unit.DirectionTo(releasePosition);
                BelongSkill.Role.Unit.Location = releasePosition;
                BelongSkill.Role.Direction = direction;
            }
        }
    }
}