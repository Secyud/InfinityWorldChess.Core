#region

using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
	public class WorldFunctions : MonoBehaviour
	{
		private void Awake()
		{
			Canvas canvas = GetComponent<Canvas>();
			canvas.worldCamera = U.Camera;
		}

		public void CallGameMenu()
		{
			GameScope.Instance.OpenGameMenu();
		}
		
		public void ResetCamera()
		{
			GameScope scope = GameScope.Instance;
			scope.Map.MapCamera.SetTargetPosition(
				scope.Player.Unit.transform.position
			);
		}
		
		public void OpenSystemMenu()
		{
			GameScope.Instance.OpenSystemMenu();
		}
	}
}