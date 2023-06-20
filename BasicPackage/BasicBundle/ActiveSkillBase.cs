using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using UnityEngine;
using UnityEngine.Playables;

namespace InfinityWorldChess.BasicBundle
{
    public abstract class ActiveSkillBase : ResourcedBase, IActiveSkill
    {
        [R(3, true)] public string ShowDescription { get; set; }
        [R(4)] public byte Score { get; set; }
        [R(5)] public byte ExecutionConsume { get; set; }
        [R(6)] public byte PositionType { get; set; }
        [R(7)] public byte PositionStart { get; set; }
        [R(8)] public byte PositionEnd { get; set; }
        [R(9)] public byte RangeType { get; set; }
        [R(10)] public byte RangeStart { get; set; }
        [R(11)] public byte RangeEnd { get; set; }
        [R(12)] public float EnergyConsume { get; set; }
        public string ShowName => Descriptor?.Name;
        public virtual SkillTargetType TargetType => SkillTargetType.Enemy;
        public virtual bool Damage => true;
        public IObjectAccessor<HexUnitPlay> UnitPlay { get; set; }
        public IObjectAccessor<Sprite> ShowIcon { get; set; }
        protected abstract string HDescription { get; }
        public int SaveIndex { get; set; }

        protected virtual string HideDescription
        {
            get
            {
                string d = HDescription;
                return (d.IsNullOrEmpty() ? "" : $" · {d}") +
                       $"\r\n · 行动力消耗: {ExecutionConsume}\r\n · 内力消耗: {EnergyConsume}";
            }
        }

        protected override void SetDefaultValue()
        {
            ShowIcon = AtlasSpriteContainer.Create(IwcAb.Instance, Descriptor, 0);
            UnitPlay = PrefabContainer<HexUnitPlay>.Create(IwcAb.Instance, Descriptor, 2);
        }

        public virtual void Release()
        {
            
        }

        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
            if (GameScope.PlayerGameContext.PlayerSetting?.WuXueQiCai == true)
            {
                transform.AddTitle3("技能信息");
                transform.AddParagraph(HideDescription);

                transform.AddParagraph(
                    " · 释放范围：" +
                    SkillRange.GetRangeInfo(PositionType, PositionStart, PositionEnd)
                );
                transform.AddParagraph(
                    " · 技能范围：" +
                    SkillRange.GetRangeInfo(RangeType, RangeStart, RangeEnd)
                );
            }
        }

        public virtual string CheckCastCondition(RoleBattleChess chess)
        {
            if (chess.ExecutionValue < ExecutionConsume)
                return "行动力不足，无法释放技能。";

            if (EnergyConsume > chess.Belong.EnergyValue)
                return "内力不足，无法释放技能。";

            return null;
        }

        public virtual ISkillRange GetCastPositionRange(IBattleChess battleChess)
        {
            return PositionType switch
            {
                0 => SkillRange.Point(battleChess.Unit.Location),
                1 => SkillRange.Line(
                    PositionStart, PositionEnd, battleChess.Unit.Location, battleChess.Direction
                ),
                2 => SkillRange.WideTriangle(
                    PositionStart, PositionEnd, battleChess.Unit.Location, battleChess.Direction
                ),
                3 => SkillRange.WideHalfCircle(
                    PositionStart, PositionEnd, battleChess.Unit.Location, battleChess.Direction
                ),
                4 => SkillRange.Circle(PositionStart, PositionEnd, battleChess.Unit.Location),
                _ => SkillRange.Circle(PositionStart, PositionEnd, battleChess.Unit.Location)
            };
        }

        public virtual ISkillRange GetCastResultRange(IBattleChess battleChess, HexCell castPosition)
        {
            HexDirection direction = castPosition.DirectionTo(battleChess.Unit.Location);

            return RangeType switch
            {
                0 => SkillRange.Point(castPosition),
                1 => SkillRange.Line(
                    RangeStart, RangeEnd, castPosition, direction
                ),
                2 => SkillRange.WideTriangle(
                    RangeStart, RangeEnd, castPosition, direction
                ),
                3 => SkillRange.WideHalfCircle(
                    RangeStart, RangeEnd, castPosition, direction
                ),
                4 => SkillRange.Circle(RangeStart, RangeEnd, castPosition),
                5 => SkillRange.WideTriangle(
                    RangeStart, RangeEnd, battleChess.Unit.Location, direction
                ),
                6 => SkillRange.WideHalfCircle(
                    RangeStart, RangeEnd, battleChess.Unit.Location, direction
                ),
                7 => SkillRange.Circle(RangeStart, RangeEnd, battleChess.Unit.Location),
                _ => SkillRange.Point(castPosition)
            };
        }

        public virtual void Cast(IBattleChess battleChess, HexCell releasePosition)
        {
            battleChess.Belong.EnergyValue -= EnergyConsume;
            battleChess.Belong.ExecutionValue -= ExecutionConsume;
        }
    }
}