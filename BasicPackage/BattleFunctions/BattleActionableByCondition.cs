using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleFunctions
{
    [Guid("E3640B93-FCBD-0CA3-890B-F8871380D6DA")]
    public class BattleActionableByCondition:BattleActionable
    {
        [field:S] public IActionable VictoryTrigger { get; set; }
        [field:S] public IActionable DefeatedTrigger { get; set; }
        [field:S] public IActionable DrawTrigger { get; set; }


        public override void Invoke(BattleScope target)
        {
            if (target.Battle.Victory)
            {
                VictoryTrigger?.Invoke();
            }
            else if (target.Battle.Defeated)
            {
                DefeatedTrigger?.Invoke();
            }
            else
            {
                DrawTrigger?.Invoke();
            }
        }
    }
}