#region

using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public class WorldMapComponent : HexMapRootBase
	{
		private UniversalAdditionalCameraData _uac;

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
			GameScope.WorldGameContext.SelectedChecker = GameScope.WorldGameContext.GetChecker(cell);
		}

		private void OnCellHover(HexCell cell)
		{
			GameScope.WorldGameContext.HoverChecker = GameScope.WorldGameContext.GetChecker(cell);
		}

		private void OnCellRightClick(HexCell cell)
		{
			Grid.FindPath(
				GameScope.PlayerGameContext.Unit.Location,
				cell, GameScope.PlayerGameContext.Unit
			);

			GameScope.WorldGameContext.Path = Grid.GetPath();
			
			IwcAb.Instance.ButtonGroupInk.Value.Create(
				cell, Og.DefaultProvider.Get<WorldHexCellBf>().Get.SelectVisibleFor(cell)
			);
		}
	}
}