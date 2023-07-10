using System;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.HexMap;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InfinityWorldChess.BattleDomain
{
    public partial class BattleContext
    {
        public void OnUpdate()
        {
            if (Battle.VictoryCondition.Victory ||
                Battle.VictoryCondition.Defeated)
                Battle.OnBattleFinish();
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    BattleChecker checker = GetCheckerUnderCell();
                    HoverChecker = GetChecker(checker?.Cell);
                    if (Input.GetMouseButtonDown(1))
                        SelectedChecker = checker;
                }

                switch (State)
                {
                    case BattleFlowState.Interval:
                        break;
                    case BattleFlowState.OnRound:
                        OnRoundIntervalCalculate();
                        break;
                    case BattleFlowState.Control:
                        if (PlayerControl)
                            OnPlayerControlUpdate();
                        else
                            OnAiControlUpdate();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private BattleChecker GetCheckerUnderCell()
        {
            HexCell cell = Map.GetCellUnderCursor();
            return GetChecker(cell);
        }

        private void OnRoundIntervalCalculate()
        {
            State = BattleFlowState.Interval;

            OnRoundEnd();
            RoleBattleChess roleBattleChess = Roles.First();
            float min = float.MaxValue;
            foreach (RoleBattleChess r in Roles)
                if (r.Time < min)
                {
                    roleBattleChess = r;
                    min = r.Time;
                }

            roleBattleChess.Time += roleBattleChess.GetTimeAdd();
            roleBattleChess.ExecutionValue += roleBattleChess.ExecutionRecoverValue;
            roleBattleChess.EnergyValue += roleBattleChess.EnergyRecoverValue;
            TotalTime = min;
            CurrentRole = roleBattleChess;

            OnRoundBegin();

            if (PlayerControl)
                EnterPlayerControl();
            else
                EnterAiControl();
        }

        private void OnPlayerControlUpdate()
        {
            BattleChecker checker = GetCheckerUnderCell();
            if (checker is null)
                return;
            if (Input.GetMouseButtonDown(0) &&
                _skillPositionCheckers.Contains(checker))
                StartCurrentSkillCast(checker.Cell);
            SetHoverRange(checker);
        }

        private void OnAiControlUpdate()
        {
            switch (Controller.State)
            {
                case AiControlState.StartPonder:
                    EnterAiControl();
                    break;
                case AiControlState.InPondering:
                    break;
                case AiControlState.FinishPonder:
                    Controller.TryInvokeCurrentNode();
                    break;
                case AiControlState.NoActionValid:
                    ExitAiControl();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StartUnitPlayBroadcast(
            IBattleChess chess, HexUnitPlay asset,HexCell target)
        {
            asset.Play(chess.Unit,target);
        }

        private void FinishUnitPlayBroadcast()
        {
            if (PlayerControl)
                EnterPlayerControl();
            else
                EnterAiControl();
        }

        public void EnterAiControl()
        {
            Ui.StartCoroutine(Controller.StartPondering());
            Ui.BattleRoleActiveMessage.OnInitialize(CurrentRole);

            Map.MapCamera.SetTargetPosition(CurrentRole.Unit.Location.Position);
            State = BattleFlowState.Control;
        }

        public void EnterPlayerControl()
        {
            Ui.BattlePlayerController.gameObject.SetActive(true);
            Ui.BattleRoleActiveMessage.OnInitialize(CurrentRole);
            SelectedChecker = SelectedChecker;
            CurrentSkill = GetSkillAvailable();

            Map.MapCamera.SetTargetPosition(CurrentRole.Unit.Location.Position);
            State = BattleFlowState.Control;
        }

        public void HidePlayerControl()
        {
            Ui.BattlePlayerController.gameObject.SetActive(false);
        }

        public void ExitPlayerControl()
        {
            HidePlayerControl();
            OnRoundIntervalCalculate();
        }

        public void ExitAiControl()
        {
            OnRoundIntervalCalculate();
        }

        public void StartCurrentSkillCast(HexCell cell)
        {
            RoleBattleChess role = CurrentRole;
            SkillContainer skill = CurrentSkill;

            HexUnitPlay pa = skill.Skill.UnitPlay?.Value;
            if (pa)
            {
                HexUnitPlay play = pa.Instantiate(role.Unit.transform);
                StartUnitPlayBroadcast(role, play,cell);
            }

            switch (skill)
            {
                case CoreSkillContainer coreSkillContainer:
                    role.SetCoreSkillCall((byte)(coreSkillContainer.EquipCode / 4));
                    break;
                case FormSkillContainer formSkillContainer:
                    role.SetFormSkillCall(formSkillContainer.EquipCode);
                    break;
            }

            HexCell selfCell = role.Unit.Location;
            if (selfCell != cell)
                role.Direction = cell.DirectionTo(selfCell);

            skill.Skill.Cast(role, cell);

            Ui.BattleFloatingBroadcast.AddBroadcast(skill.Skill);
            
            if (!pa)
                FinishUnitPlayBroadcast();
        }
    }
}