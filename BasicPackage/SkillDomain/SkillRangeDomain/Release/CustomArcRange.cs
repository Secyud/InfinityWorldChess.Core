using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.Release
{
    public class CustomArcRange:ISkillCastPosition
    {
        [field:S] public byte Start { get; set; }
        [field:S] public byte End { get; set; }
        [field:S] public byte Direction { get; set; }
        [field:S] public byte Range { get; set; }
        [field:S] public bool IncludeUnit { get; set; }
        
        public ISkillRange GetCastPositionRange(BattleUnit unit, IActiveSkill skill = null)
        {
            return SkillRange.GetArcRange(Start, End,
                unit.Location.Coordinates,
                unit.Direction + (sbyte)Direction,
                Range, IncludeUnit);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"释放范围：自定义角度({Start}-{End}, {Range}, {Direction})");
        }
    }
}