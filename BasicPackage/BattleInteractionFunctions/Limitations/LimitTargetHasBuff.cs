using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public class LimitTargetHasBuff: ILimitable<BattleInteraction>, IHasContent
    {
        [field:S] public int BuffId { get; set; }
        [field:S] public string BuffName { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"如果目标角色拥有[{BuffName}]状态，则");
        }

        public bool CheckUseful(BattleInteraction target)
        {
            return target?.Target?.Buffs[BuffId] is not null;
        }
    }
}