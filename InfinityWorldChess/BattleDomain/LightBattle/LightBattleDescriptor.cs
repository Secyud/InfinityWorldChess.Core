using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.LightBattle
{
    public class LightBattleDescriptor : BattleDescriptor
    {
        public LightBattleDescriptor(Role player, Role target)
        {
            Player = player;
            Target = target;
        }

        public override int SizeX => 4;
        public override int SizeZ => 4;

        public override IBattleVictoryCondition GenerateVictoryCondition()
        {
            return new LightBattleVictoryCondition(this);
        }

        private Role Player { get; }
        private Role Target { get; }

        public BattleRole BattlePlayer { get; private set; }
        public BattleRole BattleTarget { get; private set; }

        public override void OnBattleCreated()
        {
            BattleScope scope = BattleScope.Instance;
            HexCell cell1 = scope.Map.Grid.GetCell(2, 2);
            BattlePlayer = new BattleRole(Player)
            {
                PlayerControl = true,
                Camp = new BattleCamp()
                {
                    Color = Color.green,
                    Index = 0,
                    Name = "Player"
                }
            };
            scope.AddRoleBattleChess(BattlePlayer, cell1);
            int width = scope.Map.Grid.CellCountX;
            int height = scope.Map.Grid.CellCountZ;
            HexCell cell2 = scope.Map.Grid.GetCell(width - 3, height - 3);
            BattleTarget = new BattleRole(Target)
            {
                PlayerControl = false,
                Camp = new BattleCamp()
                {
                    Color = Color.green,
                    Index = 1,
                    Name = "Enemy"
                }
            };
            scope.AddRoleBattleChess(BattleTarget, cell2);
        }
    }
}