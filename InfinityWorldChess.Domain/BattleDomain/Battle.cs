#region

using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    public abstract class Battle : IOnBattleInitialize
    {
        public IHexCell Cell { get; set; }

        public IBattleVictoryCondition VictoryCondition { get; }

        public abstract int SizeX { get; }

        public abstract int SizeZ { get; }

        protected BattleScope Scope;

        protected Battle(IBattleVictoryCondition victoryCondition)
        {
            VictoryCondition = victoryCondition;
        }

        public virtual void OnBattleFinish()
        {
            Og.ScopeFactory.DestroyScope<BattleScope>();
            Scope = null;
        }

        public virtual void OnBattleCreate()
        {
            GameScope.OnInterrupt();
            Scope = Og.ScopeFactory.CreateScope<BattleScope>();
            Scope.CreateBattle(this);
        }

        public virtual void OnBattleInitialize()
        {
        }
    }
}