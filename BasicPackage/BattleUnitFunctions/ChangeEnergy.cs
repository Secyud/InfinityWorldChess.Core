using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleUnitFunctions
{
    [ID("02CA9FAD-3644-C429-596D-FB16080A0B63")]
    public class ChangeEnergy : IActionable<BattleUnit>, IPropertyAttached
    {
        [field: S] public float Factor { get; set; }
        [field: S] public float Value { get; set; }
        public IAttachProperty Property { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph(
                $"此招式恢复{Value}+{Factor:P0}[生]点内力。");
        }

        public void Invoke(BattleUnit role)
        {
            if (Property is not null)
            {
                role.EnergyValue += Value + Factor * Property.Living;
            }
        }
    }
}