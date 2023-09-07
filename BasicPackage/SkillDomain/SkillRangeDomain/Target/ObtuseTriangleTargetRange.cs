using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class ObtuseTriangleTargetRange :  ISkillCastResult,IHasContent
    {
        [field: S ] public int Start { get; set; }
        [field: S ] public int End { get; set; }
        [field: S ] public bool Direction { get; set; }

        public string ShowDescription => $"钝角({Start},{End})";
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition)
        {
            return SkillRange.WideTriangle(Start, End, 
                Direction?role.Unit.Location: castPosition, 
                castPosition.DirectionTo(role.Unit.Location));
        }
        
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"大三角 ({Start}-{End})");
        }
    }
}