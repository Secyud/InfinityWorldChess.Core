using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.LightBattle
{
    public class LightBattleDescriptor : IBattleDescriptor
    {
        public string ResourceId { get; set; } = "切磋";
        public LightBattleDescriptor(Role player, Role target)
        {
            Player = player;
            Target = target;
        }

        public string Description => "将目标血量降至50%以下。";
        public string Name => "切磋";
        public IObjectAccessor<Sprite> Icon => null;
        
        public WorldCell Cell { get; set; }

        public int SizeX => 4;
        public int SizeZ => 4;
        private Role Player { get; }
        private Role Target { get; }
        public BattleRole BattlePlayer { get; private set; }
        public BattleRole BattleTarget { get; private set; }

        public void OnBattleCreated()
        {
            BattleScope scope = BattleScope.Instance;
            HexCell cell1 = scope.Map.GetCell(2, 2);
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
            int width = scope.Map.CellCountX;
            int height = scope.Map.CellCountZ;
            HexCell cell2 = scope.Map.GetCell(width - 3, height - 3);
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


            BattleContext context = scope.Context;
            context.RoundBeginAction += CheckVictory;
            context.RoundEndAction += CheckVictory;
            context.ActionFinishedAction += CheckVictory;
        }

        public void OnBattleFinished()
        {
        }

        private void CheckVictory()
        {
            BattleRole target = BattleTarget;
            Victory = target.HealthValue < target.MaxHealthValue / 2;
            BattleRole player = BattlePlayer;
            Defeated = player.HealthValue < player.MaxHealthValue / 2;
        }

        public bool Victory { get; private set; }
        public bool Defeated { get; private set; }
    }
}