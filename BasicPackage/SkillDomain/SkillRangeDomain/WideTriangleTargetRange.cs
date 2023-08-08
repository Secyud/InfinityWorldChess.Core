using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain
{
    public class WideTriangleTargetRange :  ISkillCastResult,IHasContent
    {
        [field: S(ID = 0)] public int Start { get; set; }
        [field: S(ID = 1)] public int End { get; set; }

        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition)
        {
            return SkillRange.WideTriangle(Start, End, castPosition, 
                castPosition.DirectionTo(role.Unit.Location));
        }
        
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"大三角 ({Start}-{End})");
        }
    }
}