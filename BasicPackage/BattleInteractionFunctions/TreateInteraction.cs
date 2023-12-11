using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    /// <summary>
    /// run an attack.
    /// if not set property yet, use <see cref=""/> instead
    /// </summary>
    public class TreatInteraction: IActionable<BattleInteraction>, IHasContent
    {
        public virtual void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标进行治疗。");
        }
        
        public virtual void Invoke(BattleInteraction interaction)
        {
            BattleRole target = interaction.Target;
            float recover = interaction
                .GetOrAddTreat()
                .RunRecover(target);

            HexCell cell = target.Location;

            BattleScope.Instance.CreateNumberText(cell, (int)recover, Color.green);
        }
    }
}