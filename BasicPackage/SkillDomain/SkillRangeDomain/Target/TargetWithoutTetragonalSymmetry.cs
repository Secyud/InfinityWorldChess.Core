using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.Target
{
    public abstract class TargetWithoutTetragonalSymmetry : StartEndRange,IHasContent
    {
        [field: S(0)] public bool FixCenter { get; set; }
        [field: S(1)] public sbyte LocationBiasX { get; set; }
        [field: S(1)] public sbyte LocationBiasZ { get; set; }
        [field: S(2)] public sbyte DirectionBias { get; set; }

        protected override string SeLabel =>
            $"({Start},{End};{LocationBiasX},{LocationBiasZ};{DirectionBias})";

        /// <summary>
        /// 获取中心点和最终方向
        /// </summary>
        /// <param name="role"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        protected Tuple<HexCoordinates, HexDirection> GetCenter(BattleRole role, HexCell position)
        {
            HexDirection direction = role.Unit.Location.DirectionTo(position);

            HexCoordinates coordinates = role.Unit.Location.Coordinates;
            if (!FixCenter)
            {
                coordinates = position.Coordinates;
            }

            for (int i = 0; i < LocationBiasZ; i++)
            {
                coordinates += direction;
            }

            HexDirection xDirection = (HexDirection)((int)(direction + 1) % 6);

            for (int i = 0; i < LocationBiasX; i++)
            {
                coordinates += xDirection;
            }

            return new Tuple<HexCoordinates, HexDirection>(coordinates,
                (HexDirection)((int)(direction + DirectionBias) % 6));
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("释放目标：" + Description + SeLabel);
        }
    }
}