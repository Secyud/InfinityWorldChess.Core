#region

using System.Collections.Generic;
using InfinityWorldChess.GameDomain.WorldCellDomain;
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

        private readonly List<int> _path = new();

        public IList<int> Path
        {
            get => _path;
            set
            {
                foreach (int cellIndex in _path)
                {
                    if (GetCell(cellIndex) is WorldCell worldCell)
                    {
                        worldCell.PathState = 0;
                    }
                }

                _path.Clear();
                _path.AddRange(value);
                if (value.Count == 0) return;

                for (int i = 0; i < _path.Count; i++)
                {
                    if (GetCell(_path[i]) is WorldCell worldCell)
                    {
                        if (i == 0)
                        {
                            worldCell.PathState = 2;
                        }
                        else if (i == _path.Count - 1)
                        {
                            worldCell.PathState = 3;
                        }
                        else
                        {
                            worldCell.PathState = 1;
                        }
                    }
                }
            }
        }

        private void OnCellRightClick(WorldCell cell)
        {
            _mapFunction.FindPath(
                GameScope.Instance.Player.Unit.Location as WorldCell,
                cell, GameScope.Instance.Player.Unit
            );

            Path = _mapFunction.GetPath();

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