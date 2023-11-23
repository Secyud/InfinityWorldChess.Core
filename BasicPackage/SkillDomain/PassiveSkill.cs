using System;
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
        [field: S(255)] public IPassiveSkillAttached Effect { get; set; }

        public Role Role { get; set; }

        public void Install(Role target)
        {
            if (Effect is not null)
            {
                Role = target;
                Effect.Skill = this;
                Effect.Install(target);
            }
        }

        public void UnInstall(Role target)
        {
            Effect?.UnInstall(target);
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
            Effect?.SetContent(transform);
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
            this.SaveSkill(writer);
            this.SaveResource(writer);
        }

        public virtual void Load(IArchiveReader reader)
        {
            this.LoadSkill(reader);
            this.LoadResource(reader);
        }

        public int SaveIndex { get; set; }
    }
}