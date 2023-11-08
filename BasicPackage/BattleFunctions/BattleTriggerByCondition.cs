using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.NormalTriggers;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleFunctions
{
    public class BattleTriggerByCondition:BattleTrigger
    {
        [field:S] public ITrigger VictoryTrigger { get; set; }
        [field:S] public ITrigger DefeatedTrigger { get; set; }
        [field:S] public ITrigger DrawTrigger { get; set; }


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