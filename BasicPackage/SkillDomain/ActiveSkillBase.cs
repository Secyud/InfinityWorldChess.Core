﻿using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.SkillDomain.SkillRangeDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public abstract class ActiveSkillBase : IActiveSkill, IArchivable
    {
        [field: S(0)] public string ResourceId { get; set; }
        [field: S(0)] public string Name { get; set; }
        [field: S(2)] public string Description { get; set; }
        [field: S(1)] public int Score { get; set; }
        [field: S(254)] public IObjectAccessor<SkillAnim> UnitPlay { get; set; }
        [field: S(254)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(255)] public ISkillCastCondition Condition { get; set; }
        [field: S(255)] public ISkillCastPosition Position { get; set; }
        [field: S(255)] public ISkillCastResult Result { get; set; }
        [field: S(255)] public IActiveSkillEffect SkillEffect { get; set; }

        public byte Living { get; set; }
        public byte Kiling { get; set; }
        public byte Nimble { get; set; }
        public byte Defend { get; set; }

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
            if (Condition is not null)
                transform.AddParagraph($"释放要求：{Condition.Description}。");
            transform.AddParagraph($"释放范围：{Position.Description}。");
            transform.AddParagraph($"目标范围：{Result.Description}。");
            transform.AddParagraph($"效果：{SkillEffect.Description}。");
        }

        public virtual string CheckCastCondition(BattleRole chess, IActiveSkill skill)
        {
            return Condition?.CheckCastCondition(chess, skill);
        }

        public virtual void ConditionCast(BattleRole chess, IActiveSkill skill)
        {
            Condition?.ConditionCast(chess, skill);
        }

        public virtual ISkillRange GetCastPositionRange(BattleRole role, IActiveSkill skill)
        {
            if (Position is null)
                return new SkillRange(Array.Empty<BattleCell>());
            return Position.GetCastPositionRange(role, skill);
        }

        public virtual ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition, IActiveSkill skill)
        {
            if (Result is null)
                return new SkillRange(Array.Empty<BattleCell>());
            return Result.GetCastResultRange(role, castPosition, skill);
        }

        public virtual void Cast(BattleRole role, BattleCell releasePosition, ISkillRange range, IActiveSkill skill)
        {
            SkillEffect?.Cast(role, releasePosition, range, this);
        }

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