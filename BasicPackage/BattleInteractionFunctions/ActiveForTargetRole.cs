using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [Guid("8ADEBFE4-EADB-0FA7-AFB2-93A6D9E82665")]
    public class ActiveForTargetRole: IActionable<BattleInteraction>, IPropertyAttached,
        IHasContent, IHasPriority
    {
        [field:S]public int Priority { get; set; }
        [field:S]public IActionable<BattleUnit> RoleAction { get; set; }
        
        public IAttachProperty Property
        {
            get => null;
            set => value.TryAttach(RoleAction);
        }

        public void Invoke(BattleInteraction target)
        {
            RoleAction?.Invoke(target.Target);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("对目标造成效果：");
            RoleAction.TrySetContent(transform);
        }
    }
}