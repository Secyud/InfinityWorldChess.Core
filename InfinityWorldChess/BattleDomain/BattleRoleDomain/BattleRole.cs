#region

using System;
using System.Linq;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexUtilities;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    public class BattleRole : UnitProperty,
        ICanAttack, ICanDefend, IHasContent, IReleasable
    {
        private float _health, _energy;
        private int _execution;

        public TypeBuffProperty<BattleRole> TypeBuff { get; }

        public CoreSkillContainer[] NextCoreSkills { get; } =
            new CoreSkillContainer[SharedConsts.CoreSkillCodeCount];

        public FormSkillContainer[] NextFormSkills { get; } =
            new FormSkillContainer[SharedConsts.FormSkillTypeCount];

        public Role Role { get; }

        public byte CurrentCode { get; private set; }

        public byte CurrentLayer { get; private set; }

        public byte CurrentState { get; private set; } = 1;

        public bool PlayerControl { get; set; }

        public int Time { get; set; }

        public BattleCamp Camp { get; set; }

        public IdBuffProperty<BattleRole> Buff { get; }

        public void OnDying()
        {
        }

        public void OnEndPlay()
        {
        }

        public float AttackValue => Role.BodyPart[BodyType.Kiling].RealValue;

        public float DefendValue => Role.BodyPart[BodyType.Defend].RealValue;

        public float MaxHealthValue { get; private set; }

        public float MaxEnergyValue { get; private set; }

        public float EnergyRecoverValue
        {
            get
            {
                float f = Role.BodyPart[BodyType.Living].RealValue;
                return EnergyRecover * f / (f + 128);
            }
        }

        public int ExecutionRecoverValue => (int)ExecutionRecover;

        public int ExecutionValue
        {
            get => _execution;
            set => _execution = Math.Min(value, SharedConsts.MaxExecutionValue);
        }

        public float HealthValue
        {
            get => _health;
            set => _health = Math.Min(value, MaxHealthValue);
        }

        public float EnergyValue
        {
            get => _energy;
            set => _energy = Math.Min(value, MaxEnergyValue);
        }

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                SetHighlight();
            }
        }

        public HexDirection Direction
        {
            get
            {
                int d = (int)(Unit.Orientation - 30) / 60 % 6;
                if (d < 0) d += 6;
                return (HexDirection)d;
            }
            set => Unit.Orientation = (int)value * 60 + 30;
        }

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                SetHighlight();
            }
        }

        public IObjectAccessor<HexUnitAnim> UnitPlay { get; set; }
        public bool Dead { get; set; }

        private bool _active;
        private bool _selected;
        public float EnergyRecover;
        public float ExecutionRecover;

        public BattleRole(Role role)
        {
            Role = role;
            Role.CoreSkill.GetGroup(CurrentLayer, 0, NextCoreSkills);
            Role.FormSkill.GetGroup(CurrentState, NextFormSkills);
            UnitPlay = role.PassiveSkill[0]?.UnitPlay;
            TypeBuff = new TypeBuffProperty<BattleRole>(this);
            Buff = new IdBuffProperty<BattleRole>(this);
        }

        public void InitValue(float health, float energy, int execution)
        {
            MaxHealthValue = health;
            MaxEnergyValue = energy;
            _health = health;
            _energy = energy;
            _execution = execution;
        }

        public void SetCoreSkillCall(byte code)
        {
            CurrentCode = (byte)((CurrentCode << 1) + code);
            CurrentLayer = (byte)((CurrentLayer + 1) % 4);
            if (CurrentLayer == 0) CurrentCode = 0;

            Role.CoreSkill.GetGroup(CurrentLayer, CurrentCode, NextCoreSkills);

            if (CurrentLayer != 0 && NextCoreSkills.All(u => u?.Skill is null))
            {
                CurrentLayer = 0;
                CurrentCode = 0;
                Role.CoreSkill.GetGroup(CurrentLayer, 0, NextCoreSkills);
            }
        }

        /// <summary>
        /// 将变招状态置于变招类型。
        /// </summary>
        /// <param name="type"></param>
        public void SetFormSkillCall(byte type)
        {
            CurrentState = type;
            Role.FormSkill.GetGroup(CurrentState, NextFormSkills);
        }

        public bool ResetCoreSkill()
        {
            if (CurrentLayer == 0)
                return true;

            if (_execution < 1)
                return false;

            CurrentLayer = 0;
            CurrentCode = 0;
            _execution -= 1;
            return true;
        }

        public bool ResetFormSkill()
        {
            if (CurrentState == 1)
                return true;

            if (_execution < 1)
                return false;

            CurrentState = 1;
            _execution -= 1;
            return true;
        }

        public int GetTimeAdd()
        {
            const float factor = SharedConsts.BattleTimeFactor;
            float ret = factor + 4 * factor * factor / 
                (factor + Role.BodyPart[BodyType.Nimble].MaxValue);
            return Math.Max((int)ret, 1);
        }

        public void SetHighlight()
        {
            if (Unit is null) return;

            if (Selected)
                Unit.SetHighlight(Color.green);
            else if (Active)
                Unit.SetHighlight(Color.yellow);
            else
                Unit.SetHighlight(Color.white);
        }

        public int GetSpeed()
        {
            return Role.GetSpeed();
        }

        public void SetContent(Transform transform)
        {
            Role.SetContent(transform);
        }

        public void Release()
        {
            if (Unit)
            {
                Unit.Die();
            }
        }

        public override void Initialize(HexUnit unit)
        {
            base.Initialize(unit);
            SetHighlight();
            OnBattleInitialize();

            Dead = false;
        }

        public void OnBattleInitialize()
        {
            float maxHealth = Role.PassiveSkill.Living + Role.BodyPart[BodyType.Living].MaxValue;
            float maxEnergy = maxHealth;
            float execution = maxHealth / 128 + 1;
            InitValue(maxHealth, maxEnergy, (int)execution * 2);
            EnergyRecover = maxEnergy / 16;
            ExecutionRecover = execution;

            foreach (IOnBattleRoleInitialize b in Role.IdBuffs.BattleInitializes)
                b.OnBattleInitialize(this);
        }

        public TPropertyBuff GetProperty<TPropertyBuff>()
            where TPropertyBuff : class, IBuff<BattleRole>
        {
            return TypeBuff.GetOrInstall<TPropertyBuff>();
        }

        public TPropertyBuff TryGetProperty<TPropertyBuff>()
            where TPropertyBuff : class, IBuff<BattleRole>
        {
            return TypeBuff.Get<TPropertyBuff>();
        }
    }
}