using System.Runtime.InteropServices;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.GlobalFunctions
{
    [Guid("390B37AF-6042-7521-8A63-3BD484DE0654")]
    public sealed class TriggerInstallable : IInstallable, IPropertyAttached, IHasContent
    {
        [field: S] private IActionable Action { get; set; }
        [field: S] private IActionInstallTarget Target { get; set; }

        public IAttachProperty Property
        {
            get => null;
            set
            {
                value.TryAttach(Action);
                value.TryAttach(Target);
            }
        }

        public void Install()
        {
            Target.Install(Action);
        }

        public void UnInstall()
        {
            Target.UnInstall(Action);
        }

        public void SetContent(Transform transform)
        {
            Target.TrySetContent(transform);
            Action.TrySetContent(transform);
        }
    }
}