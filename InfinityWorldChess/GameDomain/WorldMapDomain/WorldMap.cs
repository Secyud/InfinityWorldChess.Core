#region

using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    public class WorldMap : HexMapRootBase
    {
        private UniversalAdditionalCameraData _uac;

        private SelectObservedService _selectObservedService;

        private void Awake()
        {
            _selectObservedService = GameScope.Instance.Get<SelectObservedService>();
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
        
        private List<HexCell> _path;

        public List<HexCell> Path
        {
            get => _path;
            set
            {
                if (!_path.IsNullOrEmpty())
                    foreach (HexCell cell in _path)
                        cell.Get<WorldCell>().PathState = 0;

                _path = value;

                if (!_path.IsNullOrEmpty())
                {
                    _path[0].Get<WorldCell>().PathState = 2;

                    for (int i = 1; i < _path.Count - 1; i++)
                        _path[i].Get<WorldCell>().PathState = 1;

                    _path.Last().Get<WorldCell>().PathState = 3;
                }
            }
        }
        
        private void OnCellRightClick(HexCell cell)
        {
            Grid.FindPath(
                GameScope.Instance.Player.Unit.Location,
                cell, GameScope.Instance.Player.Unit
            );

            Path = Grid.GetPath();

            IwcAb.Instance.ButtonGroupInk.Value.Create(
                cell, U.Get<WorldCellButtons>().Items.SelectVisibleFor(cell)
            );
        }
    }
}