using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleRoleFunctions
{
    public class AddEnergy : IActionable<BattleRole>, IPropertyAttached
    {
        [field: S] public float Factor { get; set; }
        [field: S] public float Value { get; set; }
        public IAttachProperty Property { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph(
                $"此招式恢复{Value}+{Factor:P0}[生]点内力。");
        }

        public void Invoke(BattleRole role)
        {
            if (Property is not null)
            {
                role.EnergyValue += Value + Factor * Property.Living;
            }
        }
    }
}