using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class WideHalfCircleReleaseRange :  ISkillCastPosition,IHasContent
    {
        [field: S ] public int Start { get; set; }
        [field: S ] public int End { get; set; }

        public string ShowDescription => $"半圆({Start},{End})";

        public ISkillRange GetCastPositionRange(BattleRole role)
        {
            return SkillRange.WideHalfCircle(Start, End, role.Unit.Location, role.Direction);
        }

        
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"半圆 ({Start}-{End})");
        }
    }
}