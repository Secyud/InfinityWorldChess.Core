namespace InfinityWorldChess.BattleDomain.LightBattle
{
    public class LightBattleVictoryCondition:IBattleVictoryCondition
    {
        private readonly LightBattleDescriptor _descriptor;

        public LightBattleVictoryCondition(LightBattleDescriptor descriptor)
        {
            _descriptor = descriptor;
        }

        public void OnBattleInitialize()
        {
            BattleContext context = BattleScope.Instance.Context;
            context.RoundBeginAction += CheckVictory;
            context.RoundEndAction += CheckVictory;
            context.ActionFinishedAction += CheckVictory;
        }

        private void CheckVictory()
        {
            BattleRole target = _descriptor.BattleTarget;
            Victory = target.HealthValue < target.MaxHealthValue / 2;
            BattleRole player = _descriptor.BattlePlayer;
            Defeated = player.HealthValue < player.MaxHealthValue / 2;
        }

        public string Description => "将目标血量降至50%以下。";
        public bool Victory { get;private set; }
        public bool Defeated { get;private  set; }
    }
}