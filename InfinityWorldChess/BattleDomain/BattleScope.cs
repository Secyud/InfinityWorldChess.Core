using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using JetBrains.Annotations;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMapExtensions;
using Secyud.Ugf.UgfHexMap;
using TMPro;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class BattleScope : DependencyScopeProvider
    {
        private readonly IBattleUnitInitializeService _initializeService;
        private readonly MonoContainer<BattleMap> _map;
        private readonly PrefabContainer<BattleUnit> _battleUnitPrefab;
        private readonly PrefabContainer<TextMeshPro> _simpleTextMesh;
        private readonly PrefabContainer<BattleUnitStateViewer> _stateViewer;

        private readonly MonoContainer<BattleFinishedPanel> _battleFinishPanel;

        public BattleFinishedPanel BattleFinishPanel => _battleFinishPanel.GetOrCreate();

        public static BattleScope Instance { get; private set; }
        public BattleMap Map => _map.Value;
        public IBattleDescriptor Battle { get; private set; }
        public BattleContext Context => Get<BattleContext>();

        public BattleFlowState State
        {
            get => Context.State;
            set => Context.State = value;
        }

        public BattleScope(IwcAssets assets, IBattleUnitInitializeService initializeService)
        {
            _initializeService = initializeService;
            _map = MonoContainer<BattleMap>.Create(assets, onCanvas: false);
            _battleUnitPrefab = PrefabContainer<BattleUnit>.Create(
                assets, U.TypeToPath<BattleContext>() + "Unit.prefab"
            );
            _simpleTextMesh = PrefabContainer<TextMeshPro>.Create(
                assets, "InfinityWorldChess/BattleDomain/DamageText.prefab"
            );
            _stateViewer = PrefabContainer<BattleUnitStateViewer>.Create(
                assets, "InfinityWorldChess/BattleDomain/BattleUnitDomain/BattleUnitStateViewer.prefab");
            _battleFinishPanel
                = MonoContainer<BattleFinishedPanel>.Create(assets);
        }

        public override void OnInitialize()
        {
            Instance = this;
            _map.Create();
            _map.Value.Initialize(Get<BattleHexGridDrawer>());
        }

        public BattleCell GetCell(int index)
        {
            return _map.Value.GetCell(index) as BattleCell;
        }

        public BattleCell GetCellR(int x, int z)
        {
            return _map.Value.GetCell(
                    x + HexCellExtension.Border,
                    z + HexCellExtension.Border)
                as BattleCell;
        }

        public static void CreateBattle(IBattleDescriptor descriptor)
        {
            Throw.IfNull(descriptor);

            GameScope.Instance.OnInterrupt();
            U.M.CreateScope<BattleScope>();

            Instance.Battle = descriptor;

            BattleMap grid = Instance.Map;
            grid.GenerateMap(descriptor.Cell,
                descriptor.SizeX, descriptor.SizeZ);

            descriptor.OnBattleCreated();
        }


        public static void DestroyBattle()
        {
            Transform transform = U.Canvas.transform;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).Destroy();
            }

            Instance.Context.OnBattleFinished();
            Instance.Battle.OnBattleFinished();

            U.M.DestroyScope<BattleScope>();
            GameScope.Instance.OnContinue();
        }

        public override void Dispose()
        {
            foreach (BattleUnit chess in Context.Units)
                chess.Die();
            _map.Destroy();
            Instance = null;
        }

        public BattleUnit InitBattleUnit(
            [NotNull] Role role,
            [NotNull] BattleCell cell,
            [NotNull] BattleCamp camp, bool playerControl = false)
        {
            BattleUnit unit = Object.Instantiate(_battleUnitPrefab.Value, Map.transform);

            unit.PlayerControl = playerControl;
            unit.Camp = camp;

            unit.Initialize(role, Map, cell);

            Context.Units.AddIfNotContains(unit);

            UgfUnitEffect effect = role.PassiveSkill[0]?.UnitPlay?.Instantiate();
            if (effect)
            {
                Transform transform = effect.transform;
                transform.SetParent(unit.transform);
                transform.position = unit.transform.position;
            }

            _stateViewer.Instantiate(Map.Ui.transform).Bind(unit);

            _initializeService.InitBattleUnit(unit);
            Context.OnChessAdd(unit);

            return unit;
        }

        public void KillBattleUnit([NotNull] BattleUnit chess)
        {
            chess.gameObject.SetActive(false);
            chess.Dead = true;
            Context.OnChessRemove(chess);
        }

        public void CreateNumberText([NotNull] HexCell cell, int value, Color color)
        {
            TextMeshPro t = _simpleTextMesh.Value.Instantiate();
            t.text = value.ToString();
            t.color = color;
            Map.AddBillBoard(cell, t.transform);
        }
    }
}