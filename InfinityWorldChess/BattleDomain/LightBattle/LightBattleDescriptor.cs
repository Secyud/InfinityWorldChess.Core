using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
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
        public BattleUnit BattlePlayer { get; private set; }
        public BattleUnit BattleTarget { get; private set; }

        public void OnBattleCreated()
        {
            BattleScope scope = BattleScope.Instance;
            BattleCell playerCell = scope.GetCellR(1, 1);
            BattleCamp playerCamp = new()
            {
                Color = Color.green,
                Index = 0,
                Name = "Player"
            };
            BattlePlayer = scope.InitBattleUnit(Player, playerCell, playerCamp,true);
            BattleCell enemyCell = scope.GetCellR(6, 6);
            BattleCamp enemyCamp = new()
            {
                Color = Color.red,
                Index = 1,
                Name = "Enemy"
            };
            BattleTarget = scope.InitBattleUnit(Target, enemyCell,enemyCamp);

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
            BattleUnit target = BattleTarget;
            Victory = target.HealthValue < target.MaxHealthValue / 2;
            BattleUnit player = BattlePlayer;
            Defeated = player.HealthValue < player.MaxHealthValue / 2;
        }

        public bool Victory { get; private set; }
        public bool Defeated { get; private set; }
    }
}