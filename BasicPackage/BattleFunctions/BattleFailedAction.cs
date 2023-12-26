using System.Collections.Generic;
using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleFunctions
{
    [Guid("8B62F8D7-2561-4F29-954D-0FBE4DC44195")]
    public class BattleFailedAction : BattleActionable
    {
        [field: S] public List<IActionable<BattleScope>> Triggers { get; } = new();

        public override void Invoke(BattleScope target)
        {
            if (target.Battle.Defeated)
            {
                foreach (IActionable<BattleScope> trigger in Triggers)
                {
                    trigger.Invoke(target);
                }
            }
        }
    }
}