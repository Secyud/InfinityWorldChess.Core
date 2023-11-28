using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityFunctions
{
    public class TriggerBattle:AccessorWithTemplate<IBattleDescriptor>,IActionable,IHasContent
    {
        [field: S(4)] public IActionable<BattleScope> EndBattleAction { get; set; }
        
        public void Invoke()
        {
            IBattleDescriptor battle = Accessor.Value;

            BattleScope.CreateBattle(battle);

            BattleScope instance = BattleScope.Instance;

            instance.Context.BattleFinishAction +=
                ()=> EndBattleAction.Invoke(instance);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"进行战斗{Template.Name}。");
            base.SetContent(transform);
        }
    }
}