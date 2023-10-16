using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Release
{
    public class CustomArcRance:ISkillCastPosition
    {
        [field:S] public byte Start { get; set; }
        [field:S] public byte End { get; set; }
        [field:S] public byte Direction { get; set; }
        [field:S] public byte Range { get; set; }
        [field:S] public bool IncludeUnit { get; set; }
        public string ShowDescription => $"({Start}-{End}, {Range}, {Direction})";
        public ISkillRange GetCastPositionRange(BattleRole role, IActiveSkill skill = null)
        {
            return SkillRange.GetArcRange(Start, End,
                role.Unit.Location.Coordinates,
                role.Direction + (sbyte)Direction,
                Range, IncludeUnit);
        }
    }
}