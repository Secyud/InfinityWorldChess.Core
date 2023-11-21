using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class FlashEffect : ISkillAction
    {
        public ActiveSkillBase Skill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("闪现至目标点。");
        }

        public void Invoke(BattleRole battleChess, BattleCell releasePosition)
        {
            if (!releasePosition.Unit)
            {
                HexDirection direction = Skill.Role.Unit.DirectionTo(releasePosition);
                Skill.Role.Unit.Location = releasePosition;
                Skill.Role.Direction = direction;
            }
        }
    }
}