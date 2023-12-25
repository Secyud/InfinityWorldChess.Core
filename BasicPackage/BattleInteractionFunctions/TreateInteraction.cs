using System.Runtime.InteropServices;
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
    [Guid("A4D9648B-2B11-8129-1833-79A1C782EF87")]
    public class TreatInteraction: IActionable<BattleInteraction>, IHasContent
    {
        public virtual void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标进行治疗。");
        }
        
        public virtual void Invoke(BattleInteraction interaction)
        {
            BattleUnit target = interaction.Target;
            float recover = interaction
                .GetOrAddTreat()
                .RunRecover(target);

            HexCell cell = target.Location;

            BattleScope.Instance.CreateNumberText(cell, (int)recover, Color.green);
        }
    }
}