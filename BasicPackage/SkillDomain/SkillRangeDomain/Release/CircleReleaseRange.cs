using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class CircleReleaseRange :  ISkillCastPosition,IHasContent
    {
        [field: S] public int Start { get; set; }
        [field: S] public int End { get; set; }

        public string ShowDescription => $"圆形({Start},{End})";

        public ISkillRange GetCastPositionRange(BattleRole role)
        {
            return SkillRange.Circle(Start, End, role.Unit.Location);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"自身圆形 ({Start}-{End})");
        }
    }
}