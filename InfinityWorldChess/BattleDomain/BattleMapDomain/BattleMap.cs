#region

using InfinityWorldChess.BattleDomain.BattleCellDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMapExtensions;
using Secyud.Ugf.UgfHexMap;
using Secyud.Ugf.UgfHexMapGenerator;
using UnityEngine;
using UnityEngine.Animations;

#endregion

namespace InfinityWorldChess.BattleDomain.BattleMapDomain
{
    public class BattleMap : HexGrid
    {
        [SerializeField] private LookAtConstraint BillBoardPrefab;
        [SerializeField] private Canvas Canvas;
        [SerializeField] private HexMapGenerator MapGenerator;
        public Canvas Ui => Canvas;

        private BattleControlService _controlService;

        protected override void Awake()
        {
            base.Awake();
            _controlService = U.Get<BattleControlService>();
        }

        private void Update()
        {
            if (BattleScope.Instance.Battle is null)
                return;

            if (BattleScope.Instance.BattleDescriptor.Victory ||
                BattleScope.Instance.BattleDescriptor.Defeated)
                BattleScope.DestroyBattle();
            else
            {
                HexCell cell = GetCellUnderCursor();
                if (cell.IsValid())
                {
                    _controlService.OnUpdate(cell as BattleCell);
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


        public void StartBroadcast(BattleRole role, BattleCell cell, HexUnitAnim anim)
        {
            BattleScope.Instance.State = BattleFlowState.AnimationPlay;

            if (anim)
            {
                HexUnitAnim clone = anim.Instantiate(role.Unit.transform);
                clone.Play(role.Unit as UgfUnit, cell);
            }
            else
            {
                BattleScope.Instance.State = BattleFlowState.OnEffectTrig;
            }
        }

        public void GenerateMap(WorldCell cell,int width, int height)
        {
            // TODO: use world cell to init parameter
            MapGenerator.ChunkCountX = width;
            MapGenerator.ChunkCountZ = height;
            MapGenerator.GenerateMap();
        }
    }
}