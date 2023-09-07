using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class LineTargetRange : StartEndRange, ISkillCastResult,IHasContent
    {
        public override string ShowDescription => "直线";
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition)
        {
            return SkillRange.Line(Start, End, castPosition.Coordinates, 
                castPosition.DirectionTo(role.Unit.Location));
        }
    }
}