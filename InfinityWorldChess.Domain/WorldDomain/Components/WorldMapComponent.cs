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
			GameScope.Instance.World.SelectedChecker = GameScope.Instance.World.GetChecker(cell);
		}

		private void OnCellHover(HexCell cell)
		{
			GameScope.Instance.World.HoverChecker = GameScope.Instance.World.GetChecker(cell);
		}

		private void OnCellRightClick(HexCell cell)
		{
			Grid.FindPath(
				GameScope.Instance.Player.Unit.Location,
				cell, GameScope.Instance.Player.Unit
			);

			GameScope.Instance.World.Path = Grid.GetPath();
			
			IwcAb.Instance.ButtonGroupInk.Value.Create(
				cell, U.Get<WorldHexCellBf>().Get.SelectVisibleFor(cell)
			);
		}
	}
}