using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class AcuteTriangleReleaseRange :  ISkillCastPosition,IHasContent
    {
        [field: S ] public int Start { get; set; }
        [field: S ] public int End { get; set; }

        public string ShowDescription => $"锐角({Start},{End})";

        public ISkillRange GetCastPositionRange(BattleRole role)
        {
            return SkillRange.Triangle(Start, End, role.Unit.Location, role.Direction);
        }
        
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"三角 ({Start}-{End})");
        }
    }
}