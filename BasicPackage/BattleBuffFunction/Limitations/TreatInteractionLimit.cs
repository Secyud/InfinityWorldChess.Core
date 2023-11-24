using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffFunction
{
    public class TreatInteractionLimit : 
        ILimitable<BattleInteraction>, IHasContent
    {
        public void SetContent(Transform transform)
        {
            transform.AddParagraph("如果是治疗，则");
        }

        public bool CheckUseful(BattleInteraction interaction)
        {
            return interaction?.GetTreat()?.IsRecovered == true;
        }
    }
}