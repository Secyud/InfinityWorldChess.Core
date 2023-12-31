#region

using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMapExtensions;
using Secyud.Ugf.UgfHexMap;
using Secyud.Ugf.UgfHexMapGenerator;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

#endregion

namespace InfinityWorldChess.BattleDomain
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
            if (BattleScope.Instance.Context is null)
                return;

            if (BattleScope.Instance.Battle.Victory ||
                BattleScope.Instance.Battle.Defeated)
                BattleScope.DestroyBattle();
            else if (!EventSystem.current.IsPointerOverGameObject())
            {
                BattleCell cell = GetCellUnderCursor() as BattleCell;
                if (cell.IsValid())
                {
                    _controlService.OnUpdate(cell);
                }
            }
        }

        public void AddBillBoard(HexCell cell, Transform t, float height = 10)
        {
            LookAtConstraint c = BillBoardPrefab.Instantiate();
            Vector3 position = cell.Position;
            position.y += height;
            c.transform.localPosition = position;
            c.SetSource(0, new ConstraintSource
            {
                sourceTransform = Camera.transform,
                weight = 1
            });
            t.SetParent(c.transform);
            t.localPosition = Vector3.zero;
            t.rotation = Quaternion.Euler(0, 180, 0);
        }


        public void StartBroadcast(BattleUnit unit, BattleCell cell,
            UgfUnitEffect effect, SkillEffectDelegate effectDelegate)
        {
            if (effect)
            {
                BattleScope.Instance.State = BattleFlowState.AnimationPlay;
                effectDelegate ??= SkillEffectDelegate.Default;
                effectDelegate.OnInitialize(effect, unit, cell);
            }
            else
            {
                BattleScope.Instance.Get<BattleControlService>().TriggerEffect();
            }
        }

        public void GenerateMap(WorldCell cell, int width, int height)
        {
            // TODO: use world cell to init parameter
            MapGenerator.ChunkCountX = width;
            MapGenerator.ChunkCountZ = height;
            MapGenerator.GenerateMap();
        }
    }
}