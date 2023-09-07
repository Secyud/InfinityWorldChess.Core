using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class AcuteTriangleTargetRange : TargetWithoutTetragonalSymmetry,   ISkillCastResult,IHasContent
    {
        public override string ShowDescription => "锐角";
        
        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition)
        {
            var center = GetCenter(role, castPosition);
            
            return SkillRange.Triangle(Start, End,center.Item1,center.Item2 );
        }
    }
}