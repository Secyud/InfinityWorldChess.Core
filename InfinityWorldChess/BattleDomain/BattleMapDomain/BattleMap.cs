#region

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain.AiDomain;
using InfinityWorldChess.BattleDomain.BattleCellDomain;
using InfinityWorldChess.BattleDomain.BattleRoleDomain;
using InfinityWorldChess.BattleDomain.BattleSkillDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

#endregion

namespace InfinityWorldChess.BattleDomain.BattleMapDomain
{
    public class BattleMap : HexMapRootBase
    {
        [SerializeField] private LookAtConstraint BillBoardPrefab;
        [SerializeField] private Canvas Canvas;
        public Canvas Ui => Canvas;
        public BattleFlowState State { get; set; } = BattleFlowState.OnRound;

        private HoverObservedService _hoverObservedService;
        private SelectObservedService _selectObservedService;
        private RoleObservedService _roleObservedService;
        private SkillObservedService _skillObservedService;
        private StateObservedService _stateObservedService;
        private IBattleAiController _aiController;

        private void Awake()
        {
            _hoverObservedService = U.Get<HoverObservedService>();
            _selectObservedService = U.Get<SelectObservedService>();
            _roleObservedService = U.Get<RoleObservedService>();
            _skillObservedService = U.Get<SkillObservedService>();
            _stateObservedService = U.Get<StateObservedService>();
            _aiController = U.Get<IBattleAiController>();
            _hoverObservedService.AddObserverObject(nameof(BattleMap), HoverCellRefresh, gameObject);
            _selectObservedService.AddObserverObject(nameof(BattleMap), SelectCellRefresh, gameObject);
            _roleObservedService.AddObserverObject(nameof(BattleMap), CurrentRoleRefresh, gameObject);
            _skillObservedService.AddObserverObject(nameof(BattleMap), SelectSkillRefresh, gameObject);
            _aiController = U.Get<IwcBattleAiController>();
        }

        private void Update()
        {
            if (BattleScope.Instance.Battle is null)
                return;

            if (BattleScope.Instance.VictoryCondition.Victory ||
                BattleScope.Instance.VictoryCondition.Defeated)
                U.Get<BattleGlobalService>().DestroyBattle();
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    HexCell cell = GetCellUnderCursor();
                    BattleCell message = cell ? cell.Get<BattleCell>() : null;
                    _hoverObservedService.HoverCell = message;
                    if (Input.GetMouseButtonDown(1))
                        _selectObservedService.SelectedCell = message;
                }

                switch (State)
                {
                    case BattleFlowState.Interval:
                        break;
                    case BattleFlowState.OnRound:
                        OnRoundUpdate();
                        break;
                    case BattleFlowState.Control:
                        OnControlUpdate();
                        break;
                    case BattleFlowState.SkillCast:
                        CalculateSkillEffect();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        public void AddBillBoard(HexCell cell, Transform t, float height = 10)
        {
            LookAtConstraint c = BillBoardPrefab.Instantiate(cell.transform);
            c.transform.localPosition = new Vector3(0, height, 0);
            c.SetSource(0, new ConstraintSource
            {
                sourceTransform = Camera.transform,
                weight = 1
            });
            t.SetParent(c.transform);
            t.localPosition = Vector3.zero;
            t.rotation = Quaternion.Euler(0, 180, 0);
        }

        private BattleCell _hoverCell;

        private void HoverCellRefresh()
        {
            _hoverCell?.SetHighlight();
            _hoverCell = _hoverObservedService.HoverCell;
            _hoverCell?.Cell.EnableHighlight(Color.white);
            SelectResultRefresh();
        }

        private BattleCell _selectedCell;

        private void SelectCellRefresh()
        {
            if (_selectedCell is not null)
                _selectedCell.Selected = false;
            _selectedCell = _selectObservedService.SelectedCell;
            if (_selectedCell is not null)
                _selectedCell.Selected = true;
        }

        private BattleRole _currentRole;

        private void CurrentRoleRefresh()
        {
            if (_currentRole is not null)
                _currentRole.Active = false;
            _currentRole = _roleObservedService.Role;
            if (_currentRole is not null)
            {
                _currentRole.Active = true;
            }
        }


        private readonly List<BattleCell> _skillPositionCheckers = new();
        private readonly List<BattleCell> _skillResultCheckers = new();

        private void SelectSkillRefresh()
        {
            foreach (BattleCell checker in _skillPositionCheckers)
            {
                checker.Releasable = false;
            }

            _skillPositionCheckers.Clear();
            SkillContainer skill = _skillObservedService.Skill;

            if (skill is null)
            {
                _skillResultCheckers.Clear();
                return;
            }

            BattleRole chess = _roleObservedService.Role;
            ISkillRange positions = skill.Skill.GetCastPositionRange(chess);

            foreach (HexCell area in positions.Value)
            {
                BattleCell cell = area.Get<BattleCell>();
                if (cell is null) continue;

                cell.Releasable = true;
                _skillPositionCheckers.Add(cell);
            }
        }


        private void SelectResultRefresh()
        {
            if (!_skillPositionCheckers.Any()) return;

            foreach (BattleCell checker in _skillResultCheckers)
            {
                checker.InRange = false;
            }

            _skillResultCheckers.Clear();

            if (_hoverCell is null ||
                !_skillPositionCheckers.Contains(_hoverCell)) return;

            ISkillRange range = _skillObservedService.Skill.Skill
                .GetCastResultRange(_currentRole, _hoverCell.Cell);

            foreach (HexCell area in range.Value)
            {
                BattleCell cell = area.Get<BattleCell>();
                if (cell is null) continue;

                cell.InRange = true;
                _skillResultCheckers.Add(cell);
            }
        }


        private void OnRoundUpdate()
        {
            State = BattleFlowState.Interval;

            BattleScope.Instance.Context.OnRoundEnd();
            List<BattleRole> roles = BattleScope.Instance.Context.Roles;
            BattleRole battleRole = roles.First(u => !u.Dead);
            float min = float.MaxValue;
            foreach (BattleRole r in roles)
            {
                if (!r.Dead && r.Time < min)
                {
                    battleRole = r;
                    min = r.Time;
                }
            }

            battleRole.Time += battleRole.GetTimeAdd();
            battleRole.ExecutionValue += battleRole.ExecutionRecoverValue;
            battleRole.EnergyValue += battleRole.EnergyRecoverValue;
            BattleScope.Instance.Context.TotalTime = min;

            _roleObservedService.Role = battleRole;
            _stateObservedService.Refresh();

            BattleScope.Instance.Context.OnRoundBegin();

            EnterControl();
        }

        public void EnterControl()
        {
            if (_currentRole.PlayerControl)
            {
                BattleScope.Instance.OpenPlayerControlPanel();
            }
            else
            {
                BattleScope.Instance.ClosePlayerControlPanel();
                StartCoroutine(_aiController.StartPondering());
            }
            
            MapCamera.SetTargetPosition(_currentRole.Unit.Location.Position);

            State = BattleFlowState.Control;
        }

        public void ExitControl()
        {
            State = BattleFlowState.OnRound;
        }

        private HexCell _skillCastCell;

        private void OnControlUpdate()
        {
            if (_currentRole.PlayerControl)
            {
                BattleCell cell = GetCellUnderCursor()?.Get<BattleCell>();
                if (cell is null)
                    return;
                if (Input.GetMouseButtonDown(0) &&
                    _skillPositionCheckers.Contains(cell))
                {
                    StartCurrentSkillCast(cell.Cell);
                }

                SelectResultRefresh();
            }
            else
            {
                switch (_aiController.State)
                {
                    case AiControlState.StartPonder:
                        StartCoroutine(_aiController.StartPondering());
                        break;
                    case AiControlState.InPondering:
                        break;
                    case AiControlState.FinishPonder:
                        _aiController.TryInvokeCurrentNode();
                        break;
                    case AiControlState.NoActionValid:
                        ExitControl();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void StartUnitPlayBroadcast(
            BattleRole chess, HexUnitPlay asset, HexCell target)
        {
            asset.Play(chess.Unit, target);
        }

        public void CalculateSkillEffect()
        {
            BattleRole role = _currentRole;
            SkillContainer skill = _skillObservedService.Skill;

            switch (skill)
            {
                case CoreSkillContainer coreSkillContainer:
                    role.SetCoreSkillCall((byte)(coreSkillContainer.EquipCode / 4));
                    break;
                case FormSkillContainer formSkillContainer:
                    role.SetFormSkillCall(formSkillContainer.EquipCode);
                    break;
            }

            HexCell cell = _skillCastCell;
            HexCell selfCell = role.Unit.Location;
            if (selfCell != cell)
                role.Direction = cell.DirectionTo(selfCell);
            skill.Skill.ConditionCast(role);
            ISkillRange skillRange =
                skill.Skill.GetCastResultRange(role, cell);
            skill.Skill.Cast(role, cell, skillRange);

            _stateObservedService.Refresh();
            _skillObservedService.AutoReselectSkill();

            BattleScope.Instance.Context.OnActionFinished();

            EnterControl();
        }

        public void StartCurrentSkillCast(HexCell cell)
        {
            State = BattleFlowState.Interval;
            _skillCastCell = cell;

            BattleRole battleRole = _currentRole;
            SkillContainer skill = _skillObservedService.Skill;

            HexUnitPlay pa = skill.Skill.UnitPlay?.Value;
            if (pa)
            {
                HexUnitPlay play = pa.Instantiate(battleRole.Unit.transform);
                StartUnitPlayBroadcast(battleRole, play, _skillCastCell);
            }
            else
            {
                State = BattleFlowState.SkillCast;
            }
        }
    }
}