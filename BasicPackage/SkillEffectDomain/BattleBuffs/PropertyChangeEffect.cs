using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BattleBuffs
{
    public class PropertyChangeEffect : IBuffEffect
    {
        private BattleRole _target;
        [field: S] public float Value { get; set; }
        [field: S] public byte Type { get; set; }

        private BodyType BodyType => (BodyType)Type;

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
            _target = target;
            _target.Role.BodyPart[BodyType].RealValue += Value;
        }

        public void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
            _target.Role.BodyPart[BodyType].RealValue -= Value;
        }

        public void Overlay(IBuffEffect thisEffect, IBuff<BattleRole> buff)
        {
            if (thisEffect is not PropertyChangeEffect effect)
                return;

            _target.Role.BodyPart[BodyType].RealValue += Value - effect.Value;

            effect.Value = Value;
        }
    }
}