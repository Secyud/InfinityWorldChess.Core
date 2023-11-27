using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class PassiveSkill : IPassiveSkill, IArchivable
    {
        [field: S(0)] public string ResourceId { get; set; }
        [field: S(1)] public int Score { get; set; }
        [field: S(254)] public IObjectAccessor<SkillAnimBase> UnitPlay { get; set; }
        [field: S(0)] public string Name { get; set; }
        [field: S(2)] public string Description { get; set; }
        [field: S(254)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(255)] public IInstallable<Role> Effect { get; set; }

        public Role Role { get; private set; }
        
        public void InstallFrom(Role target)
        {
            Role = target;
            if (Effect is not null)
            {
                this.TryAttach(Effect);
                Effect.InstallFrom(target);
            }
        }

        public void UnInstallFrom(Role target)
        {
            Effect?.UnInstallFrom(target);
        }

        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);

            if (GameScope.Instance.Player.PlayerSetting?.WuXueQiCai == true)
            {
                SetHideContent(transform);
            }
        }

        protected virtual void SetHideContent(Transform transform)
        {
            if (Effect is IHasContent content)
            {
                content.SetContent(transform);
            }
        }

        public byte Living { get; set; }
        public byte Kiling { get; set; }
        public byte Nimble { get; set; }
        public byte Defend { get; set; }

        public byte this[int i] => i switch
        {
            0 => Living,
            1 => Kiling,
            2 => Nimble,
            3 => Defend,
            _ => 0
        };

        public virtual void Save(IArchiveWriter writer)
        {
            this.SaveProperty(writer);
            this.SaveResource(writer);
        }

        public virtual void Load(IArchiveReader reader)
        {
            this.LoadProperty(reader);
            this.LoadResource(reader);
        }

        public int SaveIndex { get; set; }
    }
}