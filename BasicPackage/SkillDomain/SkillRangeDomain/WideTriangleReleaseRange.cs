using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain
{
    public class WideTriangleReleaseRange :  ISkillCastPosition,IHasContent
    {
        [field: S(ID = 0)] public int Start { get; set; }
        [field: S(ID = 1)] public int End { get; set; }


        public ISkillRange GetCastPositionRange(BattleRole role)
        {
            return SkillRange.WideTriangle(Start, End, role.Unit.Location, role.Direction);
        }
        
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"大三角 ({Start}-{End})");
        }
    }
}