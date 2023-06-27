#region

using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.Layout;
using System;
using UnityEngine;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public class WorldUiComponent : MonoBehaviour
	{
		[Serializable]
		public enum LeftPanel
		{
			WorldCheckerButton, WorldCheckerRole
		}

		[SerializeField] public WorldCheckerMessage WorldCheckerMessage;
		[SerializeField] public WorldCheckerButtons WorldCheckerButtons;
		[SerializeField] public WorldCheckerRoles WorldCheckerRoles;
		[SerializeField] private LayoutGroupTrigger Content;
		
		private WorldCheckerInitialize _currentPanel;

		public void CallGameMenu()
		{
			GameScope.OnRoleMessageCreation(GameScope.Instance.Player.Role, 0);
		}


		public void SelectLeftPanel(int i)
		{
			WorldCheckerInitialize panel = (LeftPanel)i switch
			{
				LeftPanel.WorldCheckerButton => WorldCheckerButtons,
				LeftPanel.WorldCheckerRole => WorldCheckerRoles,
				_ => throw new ArgumentOutOfRangeException(nameof(i), i, null)
			};
			
			if (panel == _currentPanel)
				return;

			if (_currentPanel)
				_currentPanel.gameObject.SetActive(false);
			_currentPanel = panel;
			if (_currentPanel)
			{
				_currentPanel.gameObject.SetActive(true);
				_currentPanel.OnInitialize(GameScope.Instance.Player.Role.Position);
				Content.enabled = true;
			}
		}

		public void ChangeSelectedChecker(WorldChecker checker)
		{
			WorldCheckerMessage.OnInitialize(checker);
		}

		public void ChangePlayerChecker(WorldChecker checker)
		{
			_currentPanel.OnInitialize(checker);
		}

		public void ResetCamera()
		{
			GameScope.Instance.World.Map.MapCamera.SetTargetPosition(
				GameScope.Instance.Player.Unit.transform.position
			);
		}
		public void OpenSystemMenu()
		{
			GameScope.OnSystemMenuCreation();
		}
	}
}