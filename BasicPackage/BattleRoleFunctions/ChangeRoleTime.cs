using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleRoleFunctions
{
    /// <summary>
    /// 延缓敌方行动时间
    /// </summary>
    [ID("aa8da7e7-f8a7-d7eb-9e2b-46544117c6bf")]
    public class ChangeRoleTime : IActionable<BattleRole>,IHasContent,IPropertyAttached
    {
        [field: S] public float Factor { get; set; }
        [field: S] public float Value { get; set; }
        public IAttachProperty Property { get; set; }
        
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式延缓敌方{Value}+{Factor:P0}[灵]点时序。");
        }

        public void Invoke(BattleRole target)
        {
            if (Property is not null)
            {
                target.Time += (int)(Value + Property.Nimble * Factor);
            }
        }
    }
}