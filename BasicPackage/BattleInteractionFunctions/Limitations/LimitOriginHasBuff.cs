using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public class LimitOriginHasBuff : ILimitable<BattleInteraction>, IHasContent
    {
        [field: S] public int BuffId { get; set; }
        [field: S] public string BuffName { get; set; }

        public bool CheckUseful(BattleInteraction target)
        {
            return target.Origin && target.Origin.Buffs[BuffId] is not null;
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"如果当前角色拥有[{BuffName}]状态，则");
        }
    }
}