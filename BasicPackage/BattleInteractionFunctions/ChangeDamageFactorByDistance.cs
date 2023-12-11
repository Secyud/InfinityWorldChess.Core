using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [ID("C6E92AB6-B9A3-6DB8-47D9-D6738F6604FB")]
    public class ChangeDamageFactorByDistance : IActionable<BattleInteraction>, IHasContent, IHasPriority
    {
        [field: S] public float Factor { get; set; }

        public int Priority => -0x10000;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"每单位距离{BPC.P(Factor)}此招式伤害。");
        }

        public void Invoke(BattleInteraction interaction)
        {
            HexUnit attackerUnit = interaction.Origin;
            HexUnit defenderUnit = interaction.Target;
            AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
            attackRecord.DamageFactor += Factor * attackerUnit.DistanceTo(defenderUnit);
        }
    }
}