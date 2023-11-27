using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ActivityFunctions
{
    public class TriggerBattle:IActionable
    {
        [field: S(4)] public IObjectAccessor<IBattleDescriptor> BattleAccessor { get; set; }
        [field: S(4)] public IActionable<BattleScope> EndBattleAction { get; set; }
        
        public void Invoke()
        {
            IBattleDescriptor battle = BattleAccessor.Value;

            BattleScope.CreateBattle(battle);

            BattleScope instance = BattleScope.Instance;

            instance.Context.BattleFinishAction +=
                ()=> EndBattleAction.Invoke(instance);
        }
    }
}