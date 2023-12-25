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
    /// <summary>
    /// 吸血
    /// </summary>
    [Guid("269F220D-5407-C9C8-9BD1-65D19DBFD261")]
    public class DamageTriggerTreat : IActionable<BattleInteraction>, IHasContent,IHasPriority
    {
        [field: S] private float Factor { get; set; }

        public int Priority => 0x10000;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式将造成伤害的{Factor:P0}转化为自身生命。");
        }

        public void Invoke(BattleInteraction interaction)
        {
            AttackRecordProperty attackRecord = interaction.GetAttack();
            if (attackRecord is not null)
            {
                TreatRecordProperty treatRecord = interaction.GetOrAddTreat();
                treatRecord.Treat = attackRecord.TotalDamage * Factor;
                treatRecord.RunRecover(interaction.Origin);
            }
        }
    }
}