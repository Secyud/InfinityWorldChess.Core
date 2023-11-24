using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

namespace InfinityWorldChess.BattleRoleFunctions
{
    /// <summary>
    /// 击退一格，若失败，则再攻击一次
    /// </summary>
    public class RepelOrAttackMore : IActionable<BattleInteraction>, IHasContent,IHasPriority
    {
        public int Priority => 0x8000;
        public void SetContent(Transform transform)
        {
            transform.AddParagraph("击退敌方一格，若因阻挡而无法击退，则再次对敌方造成伤害。");
        }

        public void Invoke(BattleInteraction interaction)
        {
            HexCell lC = interaction.Origin.Unit.Location;
            HexCell tC = interaction.Target.Unit.Location;
            HexDirection direction = lC.DirectionTo(tC);
            HexCell neighbour = tC.GetNeighbor(direction);
            if (neighbour && !neighbour.Unit)
            {
                interaction.Target.Unit.Location = neighbour;
            }
            else if (interaction.Target is ICanDefend defender)
            {
                AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
                attackRecord.RunDamage(defender);
            }
        }
    }
}