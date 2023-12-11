using InfinityWorldChess.BattleBuffFunction;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleUnitFunctions
{
    public class LimitRoleActionable : IBuffAttached, IPropertyAttached, IHasContent,
        IActionable<BattleUnit>
    {
        [field: S] public IActionable<BattleUnit> Actionable { get; set; }
        [field: S] public ILimitable Limitable { get; set; }

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

        public void Invoke(BattleUnit target)
        {
            if (Limitable is null || Limitable.CheckUseful())
            {
                Actionable?.Invoke(target);
            }
        }
    }
}