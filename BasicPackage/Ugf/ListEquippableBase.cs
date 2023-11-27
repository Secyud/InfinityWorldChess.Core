using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
    public abstract class ListEquippableBase<TTarget> :IEquippable<TTarget>, IPropertyAttached,IHasContent
    {
        [field: S] public List<IEquippable<TTarget>> Equippables { get; } = new();

        public IAttachProperty Property
        {
            get => null;
            set
            {
                foreach (var actionable in Equippables)
                {
                    value.TryAttach(actionable);
                }
            }
        }

        public void SetContent(Transform transform)
        {
            foreach (var actionable in Equippables)
            {
                actionable.TrySetContent(transform);
            }
        }

        public void Install(TTarget target)
        {
            foreach (var equippable in Equippables)
            {
                equippable.Install(target);
            }
        }
        public void UnInstall(TTarget target)
        {
            foreach (var equippable in Equippables)
            {
                equippable.UnInstall(target);
            }
        }
    }
}