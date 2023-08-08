using InfinityWorldChess.RoleDomain;
using JetBrains.Annotations;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    public class Fight : BattleDescriptor
    {
        [NotNull] private readonly Role _self;
        private readonly Role _target;


        public override int SizeX => 2;

        public override int SizeZ => 2;
        public override IBattleVictoryCondition GenerateVictoryCondition()
        {
            return new VictoryOnBeatAllEnemies();
        }

        public Fight(
            [NotNull] Role self,
            [NotNull] Role target)
        {
            _self = self;
            _target = target;
        }

        public override void OnBattleCreated()
        {
            HexGrid grid = BattleScope.Instance.Map.Grid;
            
            int indexMax = grid.CellCountX * grid.CellCountZ;

            int cellIndex1 = Random.Range(0, indexMax);
            int cellIndex2 = Random.Range(0, indexMax - 1);
            if (cellIndex2 >= cellIndex1)
                cellIndex2++;
            BattleCamp camp1 = new()
            {
                Name = "玩家",
                Index = 0,
                Color = Color.green
            };
            BattleCamp camp2 = new()
            {
                Name = "敌人",
                Index = 1,
                Color = Color.red
            };
            BattleScope.Instance.AutoInitializeRole(
                _self, camp1, grid.GetCell(cellIndex1).Coordinates, true
            );
            BattleScope.Instance.AutoInitializeRole(
                _target, camp2, grid.GetCell(cellIndex2).Coordinates, false
            );
        }
    }
}