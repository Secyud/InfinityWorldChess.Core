using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class Activity :IActivity
    {
        [field: S(0)] public string Name { get; set; }
        [field: S(0)] public string ResourceId { get; set; }
        [field: S(1)] public string Description { get; set; }
        [field: S(32)] public IObjectAccessor<Sprite> Icon { get; set; }

        /// <summary>
        /// install provide the finish trigger,
        /// uninstall remove the trigger.
        /// </summary>
        [field: S(16)] public IInstallable Installable { get; set; }

        [field: S(4)] public byte Living { get; set; }
        [field: S(4)] public byte Kiling { get; set; }
        [field: S(4)] public byte Nimble { get; set; }
        [field: S(4)] public byte Defend { get; set; }

        public IAttachProperty Property { get; set; }

        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
            Installable.TrySetContent(transform);
        }

        public void InstallFrom()
        {
            this.TryAttach(Installable);
            Installable?.InstallFrom();
        }

        public void UnInstallFrom()
        {
            Installable?.UnInstallFrom();
        }
    }
}