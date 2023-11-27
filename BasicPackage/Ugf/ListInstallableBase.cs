using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
    public abstract class ListInstallableBase<TTarget> :IInstallable<TTarget>, IPropertyAttached,IHasContent
    {
        [field: S] public List<IInstallable<TTarget>> Equippables { get; } = new();

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

        public void InstallFrom(TTarget target)
        {
            foreach (var equippable in Equippables)
            {
                equippable.InstallFrom(target);
            }
        }
        public void UnInstallFrom(TTarget target)
        {
            foreach (var equippable in Equippables)
            {
                equippable.UnInstallFrom(target);
            }
        }
    }
}