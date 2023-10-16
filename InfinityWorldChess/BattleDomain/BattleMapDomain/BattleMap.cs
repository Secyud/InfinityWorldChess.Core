#region

using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;
using UnityEngine.Animations;

#endregion

namespace InfinityWorldChess.BattleDomain.BattleMapDomain
{
    public class BattleMap : HexGrid
    {
        [SerializeField] private LookAtConstraint BillBoardPrefab;
        [SerializeField] private Canvas Canvas;
        public Canvas Ui => Canvas;

        private BattleControlService _controlService;

        private void Awake()
        {
            _controlService = U.Get<BattleControlService>();
        }

        private void Update()
        {
            if (BattleScope.Instance.Battle is null)
                return;

            if (BattleScope.Instance.VictoryCondition.Victory ||
                BattleScope.Instance.VictoryCondition.Defeated)
                BattleScope.DestroyBattle();
            else
            {
                HexCell cell = GetCellUnderCursor();
                _controlService.OnUpdate(cell);
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


        public void StartBroadcast(BattleRole role, HexCell cell, HexUnitPlay play)
        {
            BattleScope.Instance.State = BattleFlowState.AnimationPlay;

            if (play)
            {
                HexUnitPlay clone = play.Instantiate(role.Unit.transform);
                clone.Play(role.Unit.Get<UgfUnit>(), cell.Get<UgfCell>());
            }
            else
            {
                BattleScope.Instance.State = BattleFlowState.OnEffectTrig;
            }
        }
    }
}