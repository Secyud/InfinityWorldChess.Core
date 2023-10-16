using System;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain.Target
{
    public class TargetWithoutTetragonalSymmetry:StartEndRange
    {
        [field: S ] public bool FixCenter { get; set; }
        [field: S ] public sbyte Bias { get; set; }

        protected override string SeLabel =>  $"({Start},{End},{(FixCenter?'#':Bias.ToString())})";

        protected Tuple<HexCoordinates,HexDirection> GetCenter(BattleRole role,HexCell position)
        {
            HexDirection direction = position.DirectionTo(role.Unit.Location);
            HexCoordinates coordinates = role.Unit.Location.Coordinates;
            if (!FixCenter)
            {
                coordinates = position.Coordinates;
            }

            return new Tuple<HexCoordinates, HexDirection>( coordinates,direction);
        }
        
    }
}