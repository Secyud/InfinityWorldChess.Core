using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    /// <summary>
    /// 击退一格，若失败，则再攻击一次
    /// </summary>
    [ID("B00074E4-5E59-2176-37E3-E08FFD980BF5")]
    public class RepelTargetOrExecuteSub : IActionable<BattleInteraction>, IHasContent, IHasPriority, IPropertyAttached
    {
        [field: S] public IActionable<BattleInteraction> Actionable { get; set; }
        public int Priority => 0x8000;

        public IAttachProperty Property
        {
            get => null;
            set => value.TryAttach(Actionable);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("击退敌方一格，若因阻挡而无法击退，则");
            Actionable.TrySetContent(transform);
        }

        public void Invoke(BattleInteraction interaction)
        {
            HexCell lC = interaction.Origin.Unit.Location;
            HexCell tC = interaction.Target.Unit.Location;
            HexDirection direction = lC.DirectionTo(tC);
            HexCell neighbour = tC.GetNeighbor(direction);
            if (neighbour is not null  && !neighbour.Unit)
            {
                interaction.Target.Unit.Location = neighbour;
            }
            else
            {
                Actionable?.Invoke(interaction);
            }
        }
    }
}