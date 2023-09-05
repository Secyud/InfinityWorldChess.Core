using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class WideHalfCircleTargetRange : ISkillCastResult, IHasContent
    {
        [field: S] public int Start { get; set; }
        [field: S] public int End { get; set; }

        public string ShowDescription => $"半圆({Start},{End})";

        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition)
        {
            return SkillRange.WideHalfCircle(Start, End, castPosition,
                castPosition.DirectionTo(role.Unit.Location));
        }


        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"半圆 ({Start}-{End})");
        }
    }
}