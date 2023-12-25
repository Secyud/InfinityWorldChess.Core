using System;
using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("A212F1E8-9185-5D2A-D6EA-D258C98F52AA")]
    public abstract class ActiveSkillBase : IActiveSkill, IArchivable
    {
        [field: S(0)] public string ResourceId { get; set; }
        [field: S(0)] public string Name { get; set; }
        [field: S(2)] public string Description { get; set; }
        [field: S(1)] public int Score { get; set; }
        [field: S(15)] public byte ConditionCode { get; set; }
        [field: S(15)] public byte ConditionMask { get; set; }
        [field: S(253)] public PrefabContainer<UgfUnitEffect> UnitPlay { get; set; }

        [field: S(253)] public SkillEffectDelegate EffectDelegate { get; set; }
        [field: S(254)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(255)] public ISkillCastCondition Condition { get; set; }
        [field: S(255)] public ISkillCastPosition Position { get; set; }
        [field: S(255)] public ISkillCastResult Result { get; set; }
        [field: S(256)] public ISkillTargetInRange TargetGetter { get; set; }
        [field: S(257)] public IActionable<BattleUnit> PreSkill { get; set; }
        [field: S(258)] public IActionable<BattleInteraction> PreInteraction { get; set; }
        [field: S(259)] public IActionable<BattleInteraction> OnInteraction { get; set; }
        [field: S(260)] public IActionable<BattleInteraction> PostInteraction { get; set; }
        [field: S(261)] public IActionable<BattleUnit> PostSkill { get; set; }

        public BattleUnit Role { get; private set; }
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

        public ISkillTarget GetTargetInRange(BattleUnit battleChess, ISkillRange range)
        {
            return TargetGetter?.GetTargetInRange(battleChess, range);
        }

        protected virtual void SetHideContent(Transform transform)
        {
            Condition?.SetContent(transform);
            Position?.SetContent(transform);
            Result?.SetContent(transform);
            TargetGetter?.SetContent(transform);

            PreSkill.TrySetContent(transform);
            PreInteraction.TrySetContent(transform);
            OnInteraction.TrySetContent(transform);
            PostInteraction.TrySetContent(transform);
            PostSkill.TrySetContent(transform);
        }

        public virtual string CheckCastCondition(BattleUnit chess, IActiveSkill skill)
        {
            return Condition?.CheckCastCondition(chess, this);
        }

        public virtual void ConditionCast(BattleUnit chess, IActiveSkill skill)
        {
            Condition?.ConditionCast(chess, this);
        }

        public virtual ISkillRange GetCastPositionRange(BattleUnit role, IActiveSkill skill)
        {
            return Position?.GetCastPositionRange(role, this)
                   ?? new SkillRange(Array.Empty<BattleCell>());
        }

        public virtual ISkillRange GetCastResultRange(BattleUnit role, BattleCell castPosition, IActiveSkill skill)
        {
            return Result?.GetCastResultRange(role, castPosition, this)
                   ?? new SkillRange(Array.Empty<BattleCell>());
        }

        public virtual void Cast(BattleUnit role, BattleCell releasePosition, ISkillRange range, IActiveSkill skill)
        {
            Role = role;
            Cell = releasePosition;
            Range = range;
            Targets = GetTargetInRange(role, range);

            this.TryAttach(PreSkill);
            this.TryAttach(PreInteraction);
            this.TryAttach(OnInteraction);
            this.TryAttach(PostInteraction);
            this.TryAttach(PostSkill);

            PreSkill?.Invoke(role);
            if (Targets is not null)
            {
                foreach (BattleUnit enemy in Targets.Value)
                {
                    BattleInteraction interaction = BattleInteraction.Create(role, enemy);
                    PreInteraction?.Invoke(interaction);
                    interaction.BeforeHit();
                    OnInteraction?.Invoke(interaction);
                    interaction.AfterHit();
                    PostInteraction?.Invoke(interaction);
                }
            }

            PostSkill?.Invoke(role);
            Cell = null;
        }

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