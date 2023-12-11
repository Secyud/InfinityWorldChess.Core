using System;
using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.BattleDomain
{
    public sealed class BattleProperty : BattleRecordProperty
    {
        private Role.BodyPartProperty _roleBodyPart;

        public event Action<int> LivingChangedAction;
        public event Action<int> KilingChangedAction;
        public event Action<int> NimbleChangedAction;
        public event Action<int> DefendChangedAction;

        public int Living
        {
            get => _roleBodyPart.Living.RealValue;
            set
            {
                _roleBodyPart.Living.RealValue = value;
                LivingChangedAction?.Invoke(value);
            }
        }

        public int Kiling
        {
            get => _roleBodyPart.Kiling.RealValue;
            set
            {
                _roleBodyPart.Kiling.RealValue = value;
                KilingChangedAction?.Invoke(value);
            }
        }

        public int Nimble
        {
            get => _roleBodyPart.Nimble.RealValue;
            set
            {
                _roleBodyPart.Nimble.RealValue = value;
                NimbleChangedAction?.Invoke(value);
            }
        }

        public int Defend
        {
            get => _roleBodyPart.Defend.RealValue;
            set
            {
                _roleBodyPart.Defend.RealValue = value;
                DefendChangedAction?.Invoke(value);
            }
        }

        public int this[BodyType part]
        {
            get
            {
                return part switch
                {
                    BodyType.Living => Living,
                    BodyType.Kiling => Kiling,
                    BodyType.Nimble => Nimble,
                    BodyType.Defend => Defend,
                    _               => throw new ArgumentOutOfRangeException(nameof(part), part, null)
                };
            }
            set
            {
                switch (part)
                {
                    case BodyType.Living:
                        Living = value;
                        break;
                    case BodyType.Kiling:
                        Kiling = value;
                        break;
                    case BodyType.Nimble:
                        Nimble = value;
                        break;
                    case BodyType.Defend:
                        Defend = value;
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(part), part, null);
                }
            }
        }

        public override void InstallFrom(BattleUnit target)
        {
            _roleBodyPart = target.Role.BodyPart;
        }
    }
}