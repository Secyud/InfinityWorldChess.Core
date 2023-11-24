using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public class MinusDefendFactor: IActionable<BattleInteraction>, IHasContent,IHasPriority
    {
        [field: S] public float Factor { get; set; }

        public int Priority => -0x10000;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此攻击无视敌方{Factor:P0}防御。");
        }

        public void Invoke(BattleInteraction interaction)
        {
            AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
            attackRecord.DefendFactor -= Factor;
        }
    }
}