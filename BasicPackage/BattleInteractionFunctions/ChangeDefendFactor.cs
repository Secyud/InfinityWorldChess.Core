using System.Runtime.InteropServices;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [Guid("c32a9205-1b69-757e-cdfd-e44cb6040571")]
    public class ChangeDefendFactor: IActionable<BattleInteraction>, IHasContent,IHasPriority
    {
        [field: S] public float Factor { get; set; }

        public int Priority => -0x10000;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此攻击{BPC.P(Factor)}点防御。");
        }

        public void Invoke(BattleInteraction interaction)
        {
            AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
            attackRecord.DefendFactor += Factor;
        }
    }
}