using System.Runtime.InteropServices;
using InfinityWorldChess.BattleBuffFunction;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [Guid("A9ABBB0A-7DCE-11CA-1A9C-9B9A64CE1411")]
    public class LimitInteractionActionableFull : IBuffAttached, IPropertyAttached, IHasContent,
        IActionable<BattleInteraction>
    {
        [field: S] public IActionable<BattleInteraction> Actionable { get; set; }
        [field: S] public ILimitable<BattleInteraction> Limitable { get; set; }

        public IBattleUnitBuff Buff
        {
            get => null;
            set
            {
                value.TryAttach(Actionable);
                value.TryAttach(Limitable);
            }
        }

        public IAttachProperty Property
        {
            get => null;
            set
            {
                value.TryAttach(Actionable);
                value.TryAttach(Limitable);
            }
        }

        public void SetContent(Transform transform)
        {
            Limitable.TrySetContent(transform);
            Actionable.TrySetContent(transform);
        }

        public void Invoke(BattleInteraction target)
        {
            if (Limitable is null || Limitable.CheckUseful(target))
            {
                Actionable?.Invoke(target);
            }
        }
    }
}