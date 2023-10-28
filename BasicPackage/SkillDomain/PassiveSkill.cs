﻿using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class PassiveSkill : IPassiveSkill, IArchivable,IArchivedResource
    {

        [field: S] public string ResourceId { get; set; }
        [field: S] public byte Score { get; set; }
        [field: S] public IPassiveSkillEffect Effect { get; set; }
        [field: S] public IObjectAccessor<SkillAnim> UnitPlay { get; set; }
        [field: S] public string Name { get; set; }
        [field: S] public string Description { get; set; }
        [field: S] public IObjectAccessor<Sprite> Icon { get; set; }

        public void Equip(Role role,IPassiveSkill skill)
        {
            Effect?.Equip( role,this);
        }

        public void UnEquip(Role role,IPassiveSkill skill)
        {
            Effect?.UnEquip(role,this);
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
            transform.AddParagraph($"效果：{Effect.Description}。");
        }

        public byte Living { get; set; }
        public byte Kiling { get; set; }
        public byte Nimble { get; set; }
        public byte Defend { get; set; }


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
    }
}