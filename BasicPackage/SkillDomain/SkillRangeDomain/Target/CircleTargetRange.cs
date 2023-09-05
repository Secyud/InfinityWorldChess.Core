using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class CircleTargetRange : ISkillCastResult, IHasContent
    {
        [field: S] public int Start { get; set; }
        [field: S] public int End { get; set; }

        public string ShowDescription => $"圆形({Start},{End})";
        
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition)
        {
            return SkillRange.Circle(Start, End, castPosition);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"圆形 ({Start}-{End})");
        }
    }
}