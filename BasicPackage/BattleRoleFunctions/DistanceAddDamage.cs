using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BattleRoleFunctions
{
    public class DistanceAddDamage : IActionable<BattleInteraction>, IHasContent, IHasPriority
    {
        [field: S] public float Factor { get; set; }

        public int Priority => -0x10000;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"每单位距离增加此招式{Factor:P0}伤害。");
        }

        public void Invoke(BattleInteraction interaction)
        {
            HexUnit attackerUnit = interaction.Origin.Unit;
            HexUnit defenderUnit = interaction.Target.Unit;
            AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
            attackRecord.DamageFactor += Factor * attackerUnit.DistanceTo(defenderUnit);
        }
    }
}