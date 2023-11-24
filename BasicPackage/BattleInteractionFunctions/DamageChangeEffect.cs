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
    public class DamageChangeEffect : IActionable<BattleInteraction>, IHasContent,IHasPriority
    {
        [field: S] public float Factor { get; set; }

        public int Priority => -0x10000;

        public void Invoke(BattleInteraction interaction)
        {
            AttackRecordProperty attack = interaction.GetAttack();
            if (attack is not null)
            {
                attack.DamageFactor += Factor;
            }
        }

        public void SetContent(Transform transform)
        {
            if (Factor > 0)
            {
                transform.AddParagraph($"伤害增加{Factor:P0}。");
            }
            else if (Factor < 0)
            {
                transform.AddParagraph($"伤害减少{-Factor:P0}。");
            }
        }
    }
}