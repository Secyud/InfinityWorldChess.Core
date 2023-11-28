using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public class LimitInteractionIsAttacked :
        ILimitable<BattleInteraction>, IHasContent
    {
        public void SetContent(Transform transform)
        {
            transform.AddParagraph("如果具有攻击性，则");
        }

        public bool CheckUseful(BattleInteraction interaction)
        {
            return interaction?.GetAttack()?.IsAttacked == true;
        }
    }
}