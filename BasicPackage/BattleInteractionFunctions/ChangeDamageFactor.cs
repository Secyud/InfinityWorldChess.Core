#region

using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public class ChangeDamageFactor : IActionable<BattleInteraction>, IHasContent, IHasPriority
    {
        [field: S] public float Factor { get; set; }

        public int Priority => -0x10000;

        public void Invoke(BattleInteraction interaction)
        {
            AttackRecordProperty attack = interaction.GetOrAddAttack();
            attack.DamageFactor += Factor;
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"伤害{BPC.P(Factor)}。");
        }
    }
}