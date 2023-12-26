#region

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMapExtensions;
using Secyud.Ugf.HexUtilities;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    public class BattleUnit : UgfUnit, ICanAttack, ICanDefend
    {
        private const int MoveDivision = 10;

        private float _health, _energy;
        private float _execution;
        private bool _active;
        private bool _selected;
        private BattleCamp _camp;

        public CoreSkillContainer[] NextCoreSkills { get; } = new CoreSkillContainer[MainPackageConsts.CoreSkillCodeCount];
        public FormSkillContainer[] NextFormSkills { get; } = new FormSkillContainer[MainPackageConsts.FormSkillTypeCount];
        public Role Role { get; private set; }

        public byte CurrentCode { get; private set; }
        public byte CurrentLayer { get; private set; }
        public byte CurrentState { get; private set; } = 1;
        public bool PlayerControl { get; set; }
        public int Time { get; set; }

        public BattleCamp Camp
        {
            get => _camp;
            set
            {
                _camp = value;
                SetHighlight();
            }
        }
        public BattleUnitStateViewer StateViewer { get; set; }
        public BuffCollection<BattleUnit, IBattleUnitBuff> Buffs { get; private set; }
        public PropertyCollection<BattleUnit, IBattleUnitProperty> Properties { get; private set; }


        public float MaxHealthValue { get; set; }

        public float MaxEnergyValue { get; set; }

        public float EnergyRecover { get; set; }
        public float ExecutionRecover { get; set; }

        public bool Dead { get; set; }

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                SetHighlight();
            }
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
        public float AttackValue => Role.BodyPart[BodyType.Kiling].RealValue;
        public float DefendValue => Role.BodyPart[BodyType.Defend].RealValue;
        public float ExecutionValue
        {
            get => _execution;
            set => _execution = Math.Min(value, MainPackageConsts.MaxExecutionValue);
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

        public HexDirection Direction
        {
            get
            {
                int d = (int)(Orientation - 30) / 60 % 6;
                if (d < 0) d += 6;
                return (HexDirection)d;
            }
            set => Orientation = (int)value * 60 + 30;
        }

        private void Awake()
        {
            Properties = new PropertyCollection<BattleUnit, IBattleUnitProperty>(this);
            Buffs = new BuffCollection<BattleUnit, IBattleUnitBuff>(this);
        }

        public void Initialize(Role role, BattleMap map, BattleCell cell)
        {
            Role = role;
            Role.CoreSkill.GetGroup(CurrentLayer, 0, NextCoreSkills);
            Role.FormSkill.GetGroup(CurrentState, NextFormSkills);
            SetHighlight();

            AvatarEditor avatar = GetComponentInChildren<AvatarEditor>();
            if (avatar)
            {
                avatar.OnInitialize(role.Basic);
            }

            base.Initialize(role.Id, map, cell);
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
            const float factor = MainPackageConsts.BattleTimeFactor;
            float ret = factor + 4 * factor * factor /
                (factor + Role.BodyPart[BodyType.Nimble].MaxValue);
            return Math.Max((int)ret, 1);
        }

        public void SetHighlight()
        {
            if (Selected)
            {
                SetHighlight(Color.green);
            }
            else if (Active)
            {
                SetHighlight(Color.yellow);
            }
            else
            {
                SetHighlight(_camp?.Color ?? Color.white);
            }
        }

        public override void Die()
        {
            base.Die();
            Destroy(StateViewer.gameObject);
        }

        public int GetMoveCast(BattleCell cell)
        {
            return cell.DistanceTo(Location) * MoveDivision
                   / Role.BodyPart.Nimble.RealValue;
        }

        public IReadOnlyList<BattleCell> GetMoveRange()
        {
            if (ExecutionValue <= 0)
            {
                return Array.Empty<BattleCell>();
            }

            RoleBodyPart nimble = Role.BodyPart.Nimble;
            float execution = ExecutionValue;

            byte rg = (byte)Math.Min(nimble.RealValue * execution / MoveDivision, 10);

            HexGrid grid = BattleScope.Instance.Map;
            List<BattleCell> cells = new();
            List<Vector2> checks = new();
            HexCoordinates coordinate = Location.Coordinates;

            for (int i = 1; i < rg; i++)
            {
                HexCoordinates tmp = coordinate;

                for (int j = 0; j < i; j++)
                {
                    tmp += HexDirection.W;
                }

                for (int j = 0; j < 6; j++)
                {
                    HexDirection direction = (HexDirection)(j % 6);
                    for (int k = 0; k < i; k++)
                    {
                        TryAddCell(tmp);
                        tmp += direction;
                    }
                }

                TryAddCell(tmp);
            }

            return cells;

            void TryAddCell(HexCoordinates c)
            {
                if (grid.GetCell(c) is not BattleCell cell ||
                    !cell.IsValid()) return;

                Vector2 p2d = (c - Location.Coordinates).Position2D();

                const float r2 = 1.5f;
                foreach (Vector2 check in checks)
                {
                    if (check.x * p2d.x < 0 ||
                        check.y * p2d.y < 0)
                        continue;
                    float a = check.x * p2d.y - p2d.x * check.y;
                    float d = a * a / (check.x * check.x + check.y * check.y);
                    if (d < r2)
                        return;
                }

                if (cell.Unit)
                {
                    checks.Add(p2d);
                    return;
                }

                cells.Add(cell);
            }
        }
    }
}