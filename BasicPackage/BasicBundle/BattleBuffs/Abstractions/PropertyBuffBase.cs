using System;
using InfinityWorldChess.BasicBundle.BattleBuffs.Recorders;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions
{
    public abstract class PropertyBuffBase : BattleShownBuffBase
    {
        private RoleBattleChess _target;
        public float Value { get; set; }
        protected abstract BodyType Type { get; }

        public override string ShowDescription =>
            (Value > 0 ? $"增加{Value:N0}" : $"减少{-Value:N0}") + "点" +
            Type switch
            {
                BodyType.Living => "精气",
                BodyType.Kiling => "杀伐",
                BodyType.Nimble => "灵敏",
                BodyType.Defend => "防御",
                _ => throw new ArgumentOutOfRangeException()
            } + "。";


        public override void Install(RoleBattleChess target)
        {
            _target = target;
            _target.Role.BodyPart[Type].RealValue += Value;
        }

        public override void UnInstall(RoleBattleChess target)
        {
            _target.Role.BodyPart[Type].RealValue -= Value;
        }

        public override void Overlay(IBuff<RoleBattleChess> finishBuff)
        {
            if (finishBuff is not PropertyBuffBase buff)
                return;

            _target.Role.BodyPart[Type].RealValue += Value - buff.Value;

            buff.Value = Value;
        }

        public abstract class WithTimeRecord : PropertyBuffBase
        {
            public readonly TimeRecorder TimeRecorder;


            public override string ShowDescription => base.ShowDescription + TimeRecorder.Description;

            protected WithTimeRecord()
            {
                TimeRecorder = new TimeRecorder(GetType());
            }

            public override void Install(RoleBattleChess target)
            {
                base.Install(target);
                TimeRecorder.Install(target);
            }


            public override void UnInstall(RoleBattleChess target)
            {
                base.UnInstall(target);
                TimeRecorder.UnInstall();
            }

            public override void Overlay(IBuff<RoleBattleChess> finishBuff)
            {
                base.Overlay(finishBuff);

                if (finishBuff is not WithTimeRecord buff)
                    return;

                TimeRecorder.Overlay(buff.TimeRecorder);
            }
        }
    }
}