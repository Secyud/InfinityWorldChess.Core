using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.RoleFunctions
{
    public class ListRoleEquippable : IEquippable<Role>, IPropertyAttached,IHasContent
    {
        [field: S] public List<IEquippable<Role>> Equippables { get; } = new();

        private IAttachProperty _property;
        public IAttachProperty Property
        {
            get => _property;
            set
            {
                foreach (IEquippable<Role> equippable in Equippables)
                {
                    value.Attach(equippable);
                }

                _property = value;
            }
        }

        public void SetContent(Transform transform)
        {
            foreach (IEquippable<Role> equippable in Equippables)
            {
                equippable.TrySetContent(transform);
            }
        }

        public void Install(Role target)
        {
            foreach (IEquippable<Role> equippable in Equippables)
            {
                equippable.Install(target);
            }
        }

        public void UnInstall(Role target)
        {
            foreach (IEquippable<Role> equippable in Equippables)
            {
                equippable.UnInstall(target);
            }
        }
    }
}