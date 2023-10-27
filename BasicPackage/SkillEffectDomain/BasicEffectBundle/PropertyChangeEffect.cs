using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    public class PropertyChangeEffect : IBuffEffect
    {
        [field: S] public int Value { get; set; }
        [field: S] public byte Type { get; set; }

        private BodyType BodyType => (BodyType)Type;
        private BattleProperty _battleProperty;
        public string ShowDescription =>
            (Value > 0 ? $"增加{Value:N0}" : $"减少{-Value:N0}") + "点" +
            BodyType switch
            {
                BodyType.Living => "精气",
                BodyType.Kiling => "杀伐",
                BodyType.Nimble => "灵敏",
                BodyType.Defend => "防御",
                _               => throw new ArgumentOutOfRangeException()
            } + "。";


        public void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            _battleProperty = target.GetProperty<BattleProperty>();
            _battleProperty[BodyType] += Value;
        }

        public void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            _battleProperty[BodyType] -= Value;
        }

        public void Overlay(IBuffEffect sameEffect, IBuff<BattleRole> buff)
        {
            if (sameEffect is not PropertyChangeEffect effect)
                return;

            _battleProperty[BodyType] += Value - effect.Value;

            effect.Value = Value;
        }

        public void SetSkill(IActiveSkill skill)
        {
            
            
        }
    }
}