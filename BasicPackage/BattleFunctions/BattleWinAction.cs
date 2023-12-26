using System.Collections.Generic;
using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleFunctions
{
    [Guid("E3640B93-FCBD-0CA3-890B-F8871380D6DA")]
    public class BattleWinAction:BattleActionable
    {
        [field: S] public List<IActionable<BattleScope>> Triggers { get; } = new();

        public override void Invoke(BattleScope target)
        {
            if (target.Battle.Victory)
            {
                foreach (IActionable<BattleScope> trigger in Triggers)
                {
                    trigger.Invoke(target);
                }
            }
        }
    }
}