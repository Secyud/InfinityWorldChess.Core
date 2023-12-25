using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleUnitFunctions
{
    [Guid("1F290EA4-5BCC-45CB-8D2A-6C8BA7E64BA4")]
    public class ChangeHealth : IActionable<BattleUnit>, IPropertyAttached,IHasContent
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