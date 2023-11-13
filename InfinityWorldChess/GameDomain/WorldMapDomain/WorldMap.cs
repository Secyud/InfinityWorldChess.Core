#region

using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMapExtensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    public class WorldMap : HexGrid
    {
        public Transform WorldUI;

        private UniversalAdditionalCameraData _uac;

        private SelectObservedService _selectObservedService;
        private WorldMapFunctionService _mapFunction;

        protected override void Awake()
        {
            base.Awake();
            _selectObservedService = U.Get<SelectObservedService>();
            _mapFunction = U.Get<WorldMapFunctionService>();
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject() || !Camera.isActiveAndEnabled)
                return;

            WorldCell cell = GetCellUnderCursor() as WorldCell;
            if (cell.IsValid())
            {
                if (Input.GetMouseButtonDown(0))
                    OnCellLeftClick(cell);
                else if (Input.GetMouseButtonDown(1))
                    OnCellRightClick(cell);
                else
                    OnCellHover(cell);
            }
        }

        private void OnCellLeftClick(WorldCell cell)
        {
            _selectObservedService.Cell = cell;
        }

        private void OnCellHover(WorldCell cell)
        {
            _selectObservedService.HoverCell = cell;
        }

        private readonly List<WorldCell> _path = new();

        public IList<WorldCell> Path
        {
            get => _path;
            set
            {
                foreach (WorldCell cell in _path)
                {
                    cell.PathState = 0;
                }

                _path.Clear();
                _path.AddRange(value);
                if (value.Count == 0) return;
                _path[0].PathState = 2;

                for (int i = 1; i < _path.Count - 1; i++)
                    _path[i].PathState = 1;

                _path.Last().PathState = 3;
            }
        }

        private void OnCellRightClick(WorldCell cell)
        {
            _mapFunction.FindPath(
                GameScope.Instance.Player.Unit.Location as WorldCell,
                cell, GameScope.Instance.Player.Unit
            );

            Path = _mapFunction.GetPath().Cast<WorldCell>().ToList();

            IwcAssets.Instance.ButtonGroupInk.Value.Create(
                cell, U.Get<WorldCellButtons>().Items.SelectVisibleFor(cell)
            );
        }

        public override void Hide()
        {
            base.Hide();
            WorldUI.gameObject.SetActive(false);
        }

        public override void Show()
        {
            base.Show();
            WorldUI.gameObject.SetActive(true);
        }
    }
}