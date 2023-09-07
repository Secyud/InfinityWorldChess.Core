using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class CircleTargetRange :StartEndRange, ISkillCastResult, IHasContent
    {
        public override string ShowDescription => "圆形";
        
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition)
        {
            return SkillRange.Circle(Start, End, castPosition.Coordinates);
        }
    }
}