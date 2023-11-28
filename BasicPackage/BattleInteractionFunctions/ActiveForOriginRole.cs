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
    public class ActiveForOriginRole: IActionable<BattleInteraction>, IPropertyAttached,
        IHasContent, IHasPriority
    {
        [field:S]public int Priority { get; set; }
        [field:S]public IActionable<BattleRole> RoleAction { get; set; }
        
        public IAttachProperty Property
        {
            get => null;
            set => value.TryAttach(RoleAction);
        }

        public void Invoke(BattleInteraction target)
        {
            RoleAction?.Invoke(target.Origin);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("对释放者造成效果：");
            RoleAction.TrySetContent(transform);
        }
    }
}