using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
    public abstract class ListActionableBase<TTarget> :IActionable<TTarget>, IPropertyAttached,IHasContent
    {
        [field: S] public List<IActionable<TTarget>> Actionables { get; } = new();

        public IAttachProperty Property
        {
            get => null;
            set
            {
                foreach (var actionable in Actionables)
                {
                    value.TryAttach(actionable);
                }
            }
        }

        public void SetContent(Transform transform)
        {
            foreach (var actionable in Actionables)
            {
                actionable.TrySetContent(transform);
            }
        }

        public void Invoke(TTarget target)
        {
            foreach (var actionable in Actionables)
            {
                actionable.Invoke(target);
            }
        }
    }
}