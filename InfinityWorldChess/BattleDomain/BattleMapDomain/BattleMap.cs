#region

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain.BattleCellDomain;
using InfinityWorldChess.BattleDomain.BattleRoleDomain;
using InfinityWorldChess.BattleDomain.BattleSkillDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

#endregion

namespace InfinityWorldChess.BattleDomain.BattleMapDomain
{
    public class BattleMap : HexMapRootBase
    {
        [SerializeField] private LookAtConstraint BillBoardPrefab;

        private BattleFlowState _state;
        private HoverObservedService _hoverObservedService;
        private SelectObservedService _selectObservedService;
        private RoleObservedService _roleObservedService;
        private SkillObservedService _skillObservedService;
        private IBattleAiController _aiController;

        private void Awake()
        {
            _hoverObservedService.AddObserverObject(nameof(BattleMap), HoverCellRefresh, gameObject);
            _selectObservedService.AddObserverObject(nameof(BattleMap), SelectCellRefresh, gameObject);
            _roleObservedService.AddObserverObject(nameof(BattleMap), CurrentRoleRefresh, gameObject);
            _skillObservedService.AddObserverObject(nameof(BattleMap), SelectSkillRefresh, gameObject);
            _aiController = U.Get<IwcBattleAiController>();
        }

        private void Update()
        {
            if (BattleScope.Instance.VictoryCondition.Victory ||
                BattleScope.Instance.VictoryCondition.Defeated)
                U.Get<BattleGlobalService>().DestroyBattle();
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    HexCell cell = GetCellUnderCursor();
                    BattleCell message = cell.Get<BattleCell>();
                    _hoverObservedService.HoverCell = message;
                    if (Input.GetMouseButtonDown(1))
                        _selectObservedService.SelectedCell = message;
                }

                switch (_state)
                {
                    case BattleFlowState.Interval:
                        break;
                    case BattleFlowState.OnRound:
                        OnRoundUpdate();
                        break;
                    case BattleFlowState.Control:
                        OnControlUpdate();
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
                MapCamera.SetTargetPosition(_currentRole.Unit.Location.Position);
            }
        }

        private SkillContainer _selectedSkill;

        private readonly List<BattleCell> _skillPositionCheckers = new();
        private readonly List<BattleCell> _skillResultCheckers = new();

        private void SelectSkillRefresh()
        {
            _skillPositionCheckers.Clear();
            SkillContainer skill = _skillObservedService.Skill;

            if (skill is null)
            {
                _skillResultCheckers.Clear();
                return;
            }

            BattleRole chess = _roleObservedService.Role;
            ISkillRange positions = skill.Skill.GetCastPositionRange(chess);

            _skillPositionCheckers.AddRange(positions.Value.Select(u => u.Get<BattleCell>()));
        }


        private void SelectResultRefresh()
        {
            if (!_skillPositionCheckers.Any()) return;

            foreach (BattleCell checker in _skillResultCheckers)
                checker.InRange = false;

            _skillResultCheckers.Clear();

            if (_hoverCell is null ||
                !_skillPositionCheckers.Contains(_hoverCell)) return;

            ISkillRange range = _selectedSkill.Skill
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
            _state = BattleFlowState.Interval;

            BattleScope.Instance.Context.OnRoundEnd();
            List<BattleRole> roles = BattleScope.Instance.Context.Roles.Values.ToList();
            BattleRole battleRole = roles.First(u => !u.Dead);
            float min = float.MaxValue;
            foreach (BattleRole r in roles)
                if (!r.Dead && r.Time < min)
                {
                    battleRole = r;
                    min = r.Time;
                }

            battleRole.Time += battleRole.GetTimeAdd();
            battleRole.ExecutionValue += battleRole.ExecutionRecoverValue;
            battleRole.EnergyValue += battleRole.EnergyRecoverValue;
            BattleScope.Instance.Context.TotalTime = min;

            _roleObservedService.Role = battleRole;
            _selectObservedService.RefreshState();

            BattleScope.Instance.Context.OnRoundBegin();
            
            EnterControl();
        }

        public void EnterControl()
        {
            if (_currentRole.PlayerControl)
            {
            }
            else
            {
                StartCoroutine(_aiController.StartPondering());
            }

            _state = BattleFlowState.Control;
        }

        public void ExitControl()
        {
            _state = BattleFlowState.OnRound;
        }

        private void OnControlUpdate()
        {
            if (_currentRole.PlayerControl)
            {
                BattleCell cell = GetCellUnderCursor().Get<BattleCell>();
                if (cell is null)
                    return;
                if (Input.GetMouseButtonDown(0) &&
                    _skillPositionCheckers.Contains(cell))
                    StartCurrentSkillCast(cell.Cell);
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


        public void StartCurrentSkillCast(HexCell cell)
        {
            BattleRole battleRole = _currentRole;
            SkillContainer skill = _selectedSkill;

            HexUnitPlay pa = skill.Skill.UnitPlay?.Value;
            if (pa)
            {
                HexUnitPlay play = pa.Instantiate(battleRole.Unit.transform);
                StartUnitPlayBroadcast(battleRole, play, cell);
            }

            switch (skill)
            {
                case CoreSkillContainer coreSkillContainer:
                    battleRole.SetCoreSkillCall((byte)(coreSkillContainer.EquipCode / 4));
                    break;
                case FormSkillContainer formSkillContainer:
                    battleRole.SetFormSkillCall(formSkillContainer.EquipCode);
                    break;
            }

            HexCell selfCell = battleRole.Unit.Location;
            if (selfCell != cell)
                battleRole.Direction = cell.DirectionTo(selfCell);
            skill.Skill.SkillCastInvoke(battleRole);
            skill.Skill.Cast(battleRole, cell, skill.Skill.GetCastResultRange(battleRole, cell));

            _roleObservedService.RefreshState();
            _selectObservedService.RefreshState();
        }
    }
}