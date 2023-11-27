using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.RoleFunctions
{
    [ID("8BA74C93-5B48-E9E9-9F27-01B0FC09B094")]
    public class RoleBattleInitialize : IInstallable<Role>, IHasContent, IPropertyAttached
    {
        [field: S] public IActionable<BattleRole> Initialize { get; set; }

        public IAttachProperty Property
        {
            get => null;
            set => value.TryAttach(Initialize);
        }

        public void SetContent(Transform transform)
        {
            Initialize.TrySetContent(transform);
        }

        public void InstallFrom(Role target)
        {
            target.Buffs.BattleInitializes.Add(Initialize);
        }

        public void UnInstallFrom(Role target)
        {
            target.Buffs.BattleInitializes.Remove(Initialize);
        }
    }
}