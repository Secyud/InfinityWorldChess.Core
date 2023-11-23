using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.GameDomain;
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
        [field: S(15)] public byte ConditionCode { get; set; }
        [field: S(15)] public byte ConditionMask { get; set; }
        [field: S(254)] public IObjectAccessor<SkillAnimBase> UnitPlay { get; set; }
        [field: S(254)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(255)] public ISkillCastCondition Condition { get; set; }
        [field: S(255)] public ISkillCastPosition Position { get; set; }
        [field: S(255)] public ISkillCastResult Result { get; set; }
        [field: S(256)] public ISkillTargetInRange TargetGetter { get; set; }
        [field: S(257)] public ISkillActionEffect PreSkill { get; set; }
        [field: S(258)] public ISkillInteractionEffect PreInteraction { get; set; }
        [field: S(259)] public ISkillInteractionEffect OnInteraction { get; set; }
        [field: S(260)] public ISkillInteractionEffect PostInteraction { get; set; }
        [field: S(261)] public ISkillActionEffect PostSkill { get; set; }

        public BattleRole Role { get; private set; }
        public BattleCell Cell { get; private set; }
        public ISkillRange Range { get; private set; }
        public ISkillTarget Targets { get; private set; }

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
            Condition?.SetContent(transform);
            Position?.SetContent(transform);
            Result?.SetContent(transform);
            TargetGetter?.SetContent(transform);
            PreSkill?.SetContent(transform);
            PreInteraction?.SetContent(transform);
            OnInteraction?.SetContent(transform);
            PostInteraction?.SetContent(transform);
            PostSkill?.SetContent(transform);
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
            return Position?.GetCastPositionRange(role, skill)
                   ?? new SkillRange(Array.Empty<BattleCell>());
        }

        public virtual ISkillRange GetCastResultRange(BattleRole role, BattleCell castPosition, IActiveSkill skill)
        {
            return Result?.GetCastResultRange(role, castPosition, skill)
                   ?? new SkillRange(Array.Empty<BattleCell>());
        }

        public virtual void Cast(BattleRole role, BattleCell releasePosition, ISkillRange range, IActiveSkill skill)
        {
            Role = role;
            Cell = releasePosition;
            Range = range;
            Targets = TargetGetter?.GetTargetInRange(role, range);

            SetAttached(PreSkill);
            SetAttached(PreInteraction);
            SetAttached(OnInteraction);
            SetAttached(PostInteraction);
            SetAttached(PostSkill);

            PreSkill?.Invoke(role, releasePosition);
            if (Targets is not null)
            {
                foreach (BattleRole enemy in Targets.Value)
                {
                    SkillInteraction interaction = SkillInteraction.Create(role, enemy);
                    PreInteraction?.Invoke(interaction);
                    interaction.BeforeHit();
                    OnInteraction?.Invoke(interaction);
                    interaction.AfterHit();
                    PostInteraction?.Invoke(interaction);
                }
            }
            PostSkill?.Invoke(role, releasePosition);

            Cell = null;
        }

        private void SetAttached(IActiveSkillAttached attached)
        {
            if (attached is not null)
            {
                attached.BelongSkill = this;
            }
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