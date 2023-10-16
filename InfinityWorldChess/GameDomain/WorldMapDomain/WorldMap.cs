#region

using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.UgfHexMap;
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

        private void Awake()
        {
            _selectObservedService = U.Get<SelectObservedService>();
            _mapFunction = U.Get<WorldMapFunctionService>();
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject() || !Camera.isActiveAndEnabled)
                return;

            HexCell cell = GetCellUnderCursor();
            if (cell)
            {
                if (Input.GetMouseButtonDown(0))
                    OnCellLeftClick(cell);
                else if (Input.GetMouseButtonDown(1))
                    OnCellRightClick(cell);
                else
                    OnCellHover(cell);
            }
        }

        private void OnCellLeftClick(HexCell cell)
        {
            _selectObservedService.Cell = cell.Get<WorldCell>();
        }

        private void OnCellHover(HexCell cell)
        {
            _selectObservedService.HoverCell = cell.Get<WorldCell>();
        }

        private readonly List<UgfCell> _path = new();

        public IList<UgfCell> Path
        {
            get => _path;
            set
            {
                foreach (UgfCell cell in _path)
                {
                    cell.Cell.Get<WorldCell>().PathState = 0;
                }

                _path.Clear();

                if (value.Count == 0)return;
                
                _path[0].Cell.Get<WorldCell>().PathState = 2;

                for (int i = 1; i < _path.Count - 1; i++)
                    _path[i].Cell.Get<WorldCell>().PathState = 1;

                _path.Last().Cell.Get<WorldCell>().PathState = 3;
            }
        }

        private void OnCellRightClick(HexCell cell)
        {
            _mapFunction.FindPath(
                GameScope.Instance.Player.Unit.Location.Get<UgfCell>(),
                cell.Get<UgfCell>(), GameScope.Instance.Player.Unit
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