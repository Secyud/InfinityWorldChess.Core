using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.SkillDomain.SkillRangeDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public abstract class ActiveSkillBase : IActiveSkill, IArchivableShown, IArchivable
    {
        public string ShowName => Name;
        public string ShowDescription => Description;
        public IObjectAccessor<Sprite> ShowIcon => Icon;
        [field: S] public string Name { get; set; }
        [field: S] public string Description { get; set; }
        [field: S] public byte Score { get; set; }
        [field: S] public IObjectAccessor<HexUnitPlay> UnitPlay { get; set; }
        [field: S] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S] public ISkillCastCondition Condition { get; set; }
        [field: S] public ISkillCastPosition Position { get; set; }
        [field: S] public ISkillCastResult Result { get; set; }
        [field: S] public IActiveSkillEffect SkillEffect { get; set; }

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
            transform.AddParagraph($"释放要求：{Condition.ShowDescription}。");
            transform.AddParagraph($"释放范围：{Position.ShowDescription}。");
            transform.AddParagraph($"目标范围：{Result.ShowDescription}。");
            transform.AddParagraph($"效果：{SkillEffect.ShowDescription}。");
        }

        public string CheckCastCondition(BattleRole chess)
        {
            return Condition?.CheckCastCondition(chess);
        }

        public void SkillCastInvoke(BattleRole chess)
        {
        }

        public ISkillRange GetCastPositionRange(BattleRole role)
        {
            if (Position is null)
                return new SkillRange(Array.Empty<HexCell>());
            return Position.GetCastPositionRange(role);
        }

        public ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition)
        {
            if (Result is null)
                return new SkillRange(Array.Empty<HexCell>());
            return Result.GetCastResultRange(role, castPosition);
        }

        public  void Cast(BattleRole role, HexCell releasePosition, ISkillRange range)
        {
            SkillEffect?.Cast(this, role, releasePosition, range);
        }

        public virtual void Save(IArchiveWriter writer)
        {
            this.SaveSkill(writer);
            this.SaveByName(writer);
        }

        public virtual void Load(IArchiveReader reader)
        {
            this.LoadSkill(reader);
            this.LoadByName(reader);
        }
    }
}